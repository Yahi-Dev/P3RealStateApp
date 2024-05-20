using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services.Domain
{
    public interface IPropertyTypeService : IGenericService<SavePropertyTypeViewModel, BasePropertyTypeViewModel, PropertyType>
    {
        Task<List<BasePropertyTypeViewModel>> GetAllViewModel();
    }
}