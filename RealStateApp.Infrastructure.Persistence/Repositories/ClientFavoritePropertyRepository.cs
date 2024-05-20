using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class ClientFavoritePropertyRepository : GenericRepository<ClientFavoriteProperty>, IClientFavoritePropertyRepository
    {
        private readonly ApplicationContext _context;
        public ClientFavoritePropertyRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<List<ClientFavoriteProperty>> GetAllAsync()
        {
            return await _context.Set<ClientFavoriteProperty>()
                            .Include(b => b.Property)
                            .ToListAsync();
        }

        public async Task<List<ClientFavoriteProperty>> GetAllClientFavoriteProperties(string clientId)
        {
            var clientFavoriteProperty = await _context.Set<ClientFavoriteProperty>()
                                                .Where(fp => fp.ClientId == clientId)
                                                .Include(fp => fp.Property)
                                                    .ThenInclude(p => p.PropertyType)
                                                .Include(fp => fp.Property)
                                                    .ThenInclude(p => p.SaleCategory)
                                                .Include(fp => fp.Property)
                                                    .ThenInclude(p => p.Images)
                                                .ToListAsync();
            return clientFavoriteProperty;
        }

        public async Task<ClientFavoriteProperty> GetFavoriteProperty(string clientId, string code)
        {
            var property = await _context.Set<ClientFavoriteProperty>().FindAsync(clientId, code);
            return property;
        }

        public async Task<bool> PropertyIsFavorite(string clientId, string code)
        {
            var favoriteProperty = await _context.Set<ClientFavoriteProperty>().FindAsync(clientId, code);
            if(favoriteProperty != null)
                return true;

            return false;
        }

        public async Task UpdateAsync(ClientFavoriteProperty favProperty, string clientId, string propertyCode)
        {
            var entry = _context.Set<ClientFavoriteProperty>().Find(clientId, propertyCode);
            _context.Entry(entry).CurrentValues.SetValues(favProperty);
            await _context.SaveChangesAsync();
        }
    }
}
