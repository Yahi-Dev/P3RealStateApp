using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Services.Identity.User
{
    public interface IAgentService : IBaseIdentityUsersService
    {
        Task<List<AgentViewModel>> GetAgentList();
        Task<List<AgentMainMenuViewmodel>> GetAgentActiveList();
        Task<bool> DeleteUserAsync(string userId);
    }
}