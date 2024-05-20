using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Interfaces.Services.Identity
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request);
        Task<Response<int>> ChangeUserStatus(RegisterRequest request);
        Task<DtoAccounts> GetByEmail(string Email);
        Task Remove(DtoAccounts account);
        Task<List<DtoAccounts>> GetAllUsers();
        Task<AuthenticationResponse> AuthenticateWebAppAsync(AuthenticationRequest request);
        Task<Response<int>> RegisterUserAsync(RegisterRequest request, string origin, string UserRoles);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<Response<int>> ForgotPassswordAsync(ForgotPasswordRequest request, string origin);
        Task<Response<int>> ResetPasswordAsync(ResetPasswordRequest request);
        Task SingOutAsync();
        Task<DtoAccounts> FindUserWithFilters(FilterFindUser user);
        Task<DtoAccounts> GetByIdAsync(string UserId);
        Task<Response<int>> UpdateUserAsync(RegisterRequest request);
    }
}
