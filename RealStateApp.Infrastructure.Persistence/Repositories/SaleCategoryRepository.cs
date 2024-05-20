using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class SaleCategoryRepository : GenericRepository<SaleCategory>, ISaleCategoryRepository
    {
        private ApplicationContext _context;
        public SaleCategoryRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async override Task<List<SaleCategory>> GetAllAsync()
        {
            return await _context.Set<SaleCategory>()
                            .Include(b => b.Properties)
                            .ToListAsync();
        }
    }
}
