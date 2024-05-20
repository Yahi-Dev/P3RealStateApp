using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Identity.Users;

namespace RealStateApp.WebApp.Models.Agent
{
    public class AgentPropertiesWithAgentViewModel
    {
        public List<BasePropertyViewModel> Properties { get; set; }
        public BaseUserViewModel Agent { get; set; }
    }
}
