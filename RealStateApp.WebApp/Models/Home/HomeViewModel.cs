using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;

namespace RealStateApp.WebApp.Models.Home
{
    public class HomeViewModel
    {
        public List<BasePropertyViewModel> PropertyList { get; set; }
        public List<BasePropertyViewModel> Favorites { get; set; }
        public FiltersPropertiesViewModel Filter { get; set; } = null;
        public List<BasePropertyTypeViewModel> PropertyTypes { get; set; }
    }
}