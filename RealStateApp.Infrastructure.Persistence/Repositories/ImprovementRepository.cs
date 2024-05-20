using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class ImprovementRepository : GenericRepository<Improvement>, IImprovementRepository
    {
        public ApplicationContext _context;
        public ImprovementRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<List<Improvement>> GetAllAsync()
        {
            return await _context.Set<Improvement>()
                            .Include(b => b.Properties)
                            .ToListAsync();
        }
    }
}
