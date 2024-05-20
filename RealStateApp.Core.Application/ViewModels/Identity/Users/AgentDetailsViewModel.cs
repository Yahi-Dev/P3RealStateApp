using RealStateApp.Core.Application.ViewModels.Domain.Property;

namespace RealStateApp.Core.Application.ViewModels.Identity.Users
{
    public class AgentDetailsViewModel
    {
        public BaseUserViewModel Agent { get; set; }
        public List<BasePropertyViewModel> Properties { get; set; }
    }
}
