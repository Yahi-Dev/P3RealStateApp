using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.Services.Base;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.Services.Domain;
using RealStateApp.Core.Application.Services.Identity;
using System.Reflection;

namespace RealStateApp.Core.Application.IOC
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IClientFavoritePropertyService, ClientFavoritePropertyService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyImageService, PropertyImageService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<IImprovementService, ImprovementService>();
            services.AddTransient<ISaleCategoryService, SaleCategoryService>();
            services.AddTransient<IPropertyImprovementService, PropertyImprovementService>();


            services.AddTransient(typeof(IBaseIdentityUsersService), typeof(BaseIdentityUsersService));
            services.AddTransient<IAgentService, AgentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IDeveloperService, DeveloperService>();
            #endregion
        }
    }
}
