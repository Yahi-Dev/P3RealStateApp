using RealStateApp.Core.Application.Dtos.Email;

namespace RealStateApp.Core.Application.Interfaces.Services.Email
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
