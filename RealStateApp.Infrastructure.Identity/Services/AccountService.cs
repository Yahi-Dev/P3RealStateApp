
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services.Email;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RealStateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;

        public AccountService(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             IEmailService emailService,
                             IOptions<JWTSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }

        #region publicMethods
        public async Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered under Email {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credential for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed for {request.Email}";
                return response;
            }
            if (user.IsActive == false)
            {
                response.HasError = true;
                response.Error = $"Your account user {request.Email} is not active please get in contact with a manager";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Role = roleList.FirstOrDefault();

            response.IsVerified = user.EmailConfirmed;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.IdCard = user.IdCard;
            response.UserStatus = true;
            response.ImageUrl = user.ImageUrl;

            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }


        //USER AUTHENTICATION
        public async Task<AuthenticationResponse> AuthenticateWebAppAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No accounts registered under Email {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credential for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account not confirmed for {request.Email}";
                return response;
            }
            if (user.IsActive == false)
            {
                response.HasError = true;
                response.Error = $"Your account user {request.Email} is not active please get in contact with a manager";
                return response;
            }


            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Role = roleList.FirstOrDefault();

            response.IsVerified = user.EmailConfirmed;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.IdCard = user.IdCard;
            response.PhoneNumber = user.PhoneNumber;
            response.UserStatus = true;
            response.ImageUrl = user.ImageUrl;
            return response;
        }


        //SINGOUT
        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //REGISTER USER

        public async Task<Response<int>> RegisterUserAsync(RegisterRequest request, string origin, string UserRoles)
        {
            Response<int> response = new();
            response.Succeeded = true;

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.Succeeded = false;
                response.Message = $"Username {request.UserName} is already taken";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.Succeeded = false;
                response.Message = $"Email {request.Email} is already registered";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                IsActive = false,
                IdCard = request.IdCard,
                PhoneNumber = request.PhoneNumber
            };

            if(UserRoles == RolesEnum.Client.ToString() || UserRoles == RolesEnum.Developer.ToString() || UserRoles == RolesEnum.Admin.ToString())
            {
                user.IsActive = true;
            }

            var result = await _userManager.CreateAsync(user, request.Password);

            var registeredUser = await _userManager.FindByEmailAsync(user.Email);

            if (registeredUser != null && request.formFile != null)
            {
                registeredUser.ImageUrl = UploadImage.UploadFile(request.formFile, registeredUser.UserName, false);
                await _userManager.UpdateAsync(registeredUser);
            }

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles);
                
                var verificationURI = await SendVerificationUri(user, origin); if (UserRoles == RolesEnum.Developer.ToString() || UserRoles == RolesEnum.Admin.ToString() || UserRoles == RolesEnum.Agent.ToString())
                {
                    user.EmailConfirmed = true;
                    response.Message = "Cuenta Registrada";
                    var registeredUserForEmailConfirm = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.UpdateAsync(registeredUserForEmailConfirm);
                    return response;
                }
                else
                {
                    await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                    {
                        To = user.Email,
                        Body = $"{verificationURI}",
                        Subject = "Confirm registration"
                    }) ;
                }
            }
            else
            {
                response.Succeeded = false;
                response.Message = $"An error occurred trying to register the user.";
                return response;
            }

            response.Message = "Favor confirmar la cuenta.";
            return response;
        }

        //RESETPASSWORD

        public async Task<Response<int>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            Response<int> response = new();
            response.Succeeded = true;
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.Succeeded = false;
                response.Message = $"An error occurred while reset password";
                return response;
            }

            return response;
        }


        //GETBYID
        public async Task<DtoAccounts> GetByIdAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            DtoAccounts dtoaccount = new()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName,
                ImageUrl = user.ImageUrl,
                IdCard = user.IdCard,
                IsActive = user.IsActive,
                PhoneNumber = user.PhoneNumber,
                Password = user.PasswordHash
            };
            return dtoaccount;
        }



        //CONFIRMACCOUNT
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No user register under this {user.Email} account";
            }

            token = System.Text.Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirm for {user.Email} you can now  use the app";
            }
            else
            {
                return $"An error occurred wgile confirming {user.Email}.";
            }
        }

        //FORGOTPASSWORD
        public async Task<Response<int>> ForgotPassswordAsync(ForgotPasswordRequest request, string origin)
        {
            Response<int> response = new();
            response.Succeeded = true;
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationURI = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationURI}",
                Subject = "reset password"
            });

            return response;

        }
        public async Task<DtoAccounts> GetByEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            DtoAccounts dtoaccount = new()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName,
                ImageUrl = user.ImageUrl,
                IdCard = user.IdCard,
                IsActive = user.IsActive,
                PhoneNumber = user.PhoneNumber,
            };
            return dtoaccount;
        }


        //DELETE USER
        public async Task Remove(DtoAccounts account)
        {
            Response<int> response = new();

            var user = await _userManager.FindByIdAsync(account.Id);
            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"This user does not exist now";
            }
            await _userManager.DeleteAsync(user);
        }

        //USERS GETALL

        public async Task<List<DtoAccounts>> GetAllUsers()
        {
            var userList = await _userManager.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToListAsync();
            List<DtoAccounts> DtoUserList = new();

            foreach (var user in userList)
            {
                var userDto = new DtoAccounts();
                userDto.ImageUrl = user.ImageUrl;
                userDto.FirstName = user.FirstName;
                userDto.LastName = user.LastName;
                userDto.UserName = user.UserName;
                userDto.IsActive = user.IsActive;
                userDto.IdCard = user.IdCard;
                userDto.Email = user.Email;
                userDto.Id = user.Id;

                // esto no esta muy bien implemntado, se necesita elegir el role del usuario pero el metodo GetRolesAsync 
                // solo funciona con listas de string y el usuario solamente puede tener un role.

                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                userDto.Role = roles.FirstOrDefault();


                DtoUserList.Add(userDto);
            }

            return DtoUserList;
        }


        public async Task<DtoAccounts> FindUserWithFilters(FilterFindUser user)
        {
            var applicationUser = await _userManager.FindByNameAsync(user.NameAgent);
            var userDto = new DtoAccounts();

            if (string.Equals(applicationUser.FirstName, user.NameAgent, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(applicationUser.LastName, user.NameAgent, StringComparison.OrdinalIgnoreCase) && user != null)
            {
                userDto.ImageUrl = applicationUser.ImageUrl;
                userDto.FirstName = applicationUser.FirstName;
                userDto.LastName = applicationUser.LastName;
                userDto.IsActive = applicationUser.IsActive;
                userDto.IdCard = applicationUser.IdCard;
                userDto.Email = applicationUser.Email;
                userDto.Id = applicationUser.Id;

                if (userDto.IsActive == false)
                {
                    return null;
                }
                else
                {
                    return userDto;
                }
            }
            else
            {
                return null;
            }
        }

        //CHANGE USER STATUS
        public async Task<Response<int>> ChangeUserStatus(RegisterRequest request)
        {
            Response<int> response = new();
            response.Succeeded = true;
            var userget = await _userManager.FindByIdAsync(request.Id);
            {
                userget.IsActive = request.IsActive;
            }
            var result = await _userManager.UpdateAsync(userget);
            if (!result.Succeeded)
            {
                response.Succeeded = false;
                response.Message = $"There was an error while trying to update the user{userget.UserName}";
            }
            return response;
        }

        //EDITUSER
        public async Task<Response<int>> UpdateUserAsync(RegisterRequest request)
        {
            Response<int> response = new();
            response.Succeeded = true;

            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                response.Succeeded = false;
                response.Message = $"User with ID {request.Id} not found.";
                return response;
            }


            user.PhoneNumber = request.PhoneNumber;
            user.UserName = request.UserName;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.IdCard = request.IdCard;
            user.ImageUrl = request.ImageUrl;
            user.IsActive = request.IsActive;

            if (!string.IsNullOrEmpty(request.ConfirmPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, request.Password);
                if (!result.Succeeded)
                {
                    response.Succeeded = false;
                    response.Message = $"Failed to reset password for user {user.UserName}.";
                    return response;
                }
            }

            if (request.formFile != null)
            {
                user.ImageUrl = UploadImage.UploadFile(request.formFile, request.Id, false, user.ImageUrl);
                user.ImageUrl = UploadImage.UploadFile(request.formFile, "User", false, user.ImageUrl);
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                response.Succeeded = false;
                response.Message = $"Failed to update user {user.UserName}.";
            }

            return response;
        }

        #endregion

        #region PrivateMethods

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }


        private async Task<string> SendVerificationUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/Account/confirm-email";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }


        #endregion
    }
}
