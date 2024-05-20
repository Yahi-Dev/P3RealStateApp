using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services.Domain
{
    public interface IPropertyService : IGenericService<SavePropertyViewModel, BasePropertyViewModel, Property> 
    {
        Task<BasePropertyViewModel> GetPropertyByCode(FiltersPropertiesViewModel filters);
        Task<List<BasePropertyViewModel>> GetAllPropertyWithFilters(FiltersPropertiesViewModel filters);
        Task<PropertyInfoViewModel> GetInfoByIdViewModel(int id);
        Task<List<BasePropertyViewModel>> GetAllAgentProperty(string agentId);
        Task<List<BasePropertyImageViewModel>> GetPropertyImagesById(int propertyId);
        Task<BasePropertyViewModel> GetByIdAsync(int Id);
        Task RemoveAllPropertyImprovements(int propertyId);
    }
}
