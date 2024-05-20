using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyTypeRepository : GenericRepository<PropertyType>, IPropertyTypeRepository
    {
        private ApplicationContext _context;
        public PropertyTypeRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<List<PropertyType>> GetAllAsync()
        {
            return await _context.Set<PropertyType>()
                            .Include(b => b.Properties)
                            .ToListAsync();
        }
    }
}
