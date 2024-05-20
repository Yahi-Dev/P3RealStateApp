using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Infrastructure.Persistence.Contexts;
using RealStateApp.Infrastructure.Persistence.Repositories;

namespace RealStateApp.Infrastructure.Persistence.IOC
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>
                                                              options.UseInMemoryDatabase("RealStateApp"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IClientFavoritePropertyRepository, ClientFavoritePropertyRepository>();
            services.AddTransient<IImprovementRepository, ImprovementRepository>();
            services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddTransient<ISaleCategoryRepository, SaleCategoryRepository>();
            services.AddTransient<IPropertyImprovementRepository, PropertyImprovementRepository>();
            #endregion

        }
    }
}
