using AutoMapper;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountservice;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountservice, IMapper mapper)
        {
            _accountservice = accountservice;
            _mapper = mapper;
        }


        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginrequest = _mapper.Map<AuthenticationRequest>(vm);

            AuthenticationResponse userResponse = await _accountservice.AuthenticateWebAppAsync(loginrequest);
            return userResponse;
        }


        public async Task SignOutAsync()
        {
            await _accountservice.SingOutAsync();
        }



        public async Task<Response<int>> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountservice.RegisterUserAsync(registerRequest, origin, vm.Role);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountservice.ConfirmAccountAsync(userId, token);
        }


        public async Task<Response<int>> ForgotPasssowrdAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountservice.ForgotPassswordAsync(forgotRequest, origin);
        }

        public async Task<Response<int>> ResetPasssowrdAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountservice.ResetPasswordAsync(resetRequest);
        }

        public async Task<DtoAccounts> GetAgentByFilter(FilterFindUser user)
        {
            return await _accountservice.FindUserWithFilters(user);
        }
    }
}
