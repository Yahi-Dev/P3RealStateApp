using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyImprovementRepository : GenericRepository<PropertyImprovement>, IPropertyImprovementRepository
    {
        public ApplicationContext _context;
        public PropertyImprovementRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<List<PropertyImprovement>> GetAllAsync()
        {
            return await _context.Set<PropertyImprovement>()
                            .Include(e => e.Property)
                            .Include(e=>e.Improvement)
                            .ToListAsync();
        }

        public async Task<List<PropertyImprovement>> GetByPropertyId(int id)
        {
            var propertyImprovements = await _context.Set<PropertyImprovement>().Where(pi => pi.PropertyId == id).ToListAsync();

            return propertyImprovements;
        }
    }
}
