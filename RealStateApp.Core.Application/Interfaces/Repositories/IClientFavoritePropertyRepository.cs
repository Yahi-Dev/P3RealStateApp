using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IClientFavoritePropertyRepository : IGenericRepository<ClientFavoriteProperty>
    {
        Task<List<ClientFavoriteProperty>> GetAllClientFavoriteProperties(string clientId);
        Task<ClientFavoriteProperty> GetFavoriteProperty(string clientId, string code);
        Task<bool> PropertyIsFavorite(string clientId, string code);
        Task UpdateAsync(ClientFavoriteProperty favProperty, string clientId, string propertyCode);

    }
}
