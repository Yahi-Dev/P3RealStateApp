using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImage>, IPropertyImageRepository
    {
        private readonly ApplicationContext _context;
        public PropertyImageRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
