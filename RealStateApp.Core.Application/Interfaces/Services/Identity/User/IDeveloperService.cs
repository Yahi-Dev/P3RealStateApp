using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;

namespace RealStateApp.Core.Application.Interfaces.Services.Identity.User
{
    public interface IDeveloperService : IBaseIdentityUsersService
    {
        Task<List<BaseUserViewModel>> GetListDeveloper();
    }
}
