using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Interfaces.Services.Identity.User
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<Response<int>> ForgotPasssowrdAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<DtoAccounts> GetAgentByFilter(FilterFindUser user);
        Task<Response<int>> RegisterAsync(SaveUserViewModel vm, string origin);
        Task<Response<int>> ResetPasssowrdAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();
    }
}
