using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository : GenericRepository<Core.Domain.Entities.Property>, IPropertyRepository
    {
        private ApplicationContext _context;
        public PropertyRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PropertyImage>> GetPropertyImagesById(int propertyId)
        {
            return await _context.Set<PropertyImage>()
                .Where(i => i.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<List<Core.Domain.Entities.Property>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _context.Set<Core.Domain.Entities.Property>().AsQueryable();

            foreach (string property in properties)
            {
                query = query.Include(property);
            }
            var result = await query.ToListAsync();
            return result;
        }
    }
}
