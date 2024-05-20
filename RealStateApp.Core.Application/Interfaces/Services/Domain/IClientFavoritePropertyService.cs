using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.ClientFavoriteProperty;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Services.Domain
{
    public interface IClientFavoritePropertyService : IGenericService<SaveClientFavoritePropertyViewModel, BaseClientFavoritePropertyViewModel, ClientFavoriteProperty> 
    {
        Task<List<BasePropertyViewModel>> GetAllFavoriteProperty(string ClientId);
    }
}
