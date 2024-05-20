using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services.Domain
{
    public interface IPropertyImageService : IGenericService<SavePropertyImageViewModel, BasePropertyImageViewModel, PropertyImage>
    {
    }
}
