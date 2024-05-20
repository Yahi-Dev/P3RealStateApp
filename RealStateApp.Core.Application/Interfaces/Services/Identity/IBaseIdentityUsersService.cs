using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Interfaces.Services.Identity
{
    public interface IBaseIdentityUsersService
    {
        Task<Response<int>> ChangeStatusAsync(SaveChangeStatusViewModel vm);
        Task<Response<int>> CreateAsync(SaveUserViewModel viewModel, string id, string origin, string role);
        Task<Response<int>> DeleteByIdAsync(string id);
        Task<Response<List<BaseUserViewModel>>> GetAllAsync();
        Task<Response<BaseUserViewModel>> GetByIdAsync(string id);
        Task<Response<int>> UpdateAsync(SaveUserViewModel viewModel, string id);
    }
}