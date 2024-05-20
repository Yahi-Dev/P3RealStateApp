using RealStateApp.Core.Application.ViewModels.Domain.Improvement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.Domain.Property
{
    public class PropertyInfoViewModel
    {
        public BasePropertyViewModel Property { get; set; }

        public BaseUserViewModel Agent { get; set; }
    }
}
