using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.Services.Email;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infrastructure.Shared.Services;

namespace RealStateApp.Infrastructure.Shared.IOC
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
