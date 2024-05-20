using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Helpers;

namespace RealStateApp.WebApp.Middelwares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor httpContext;


        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse vm = httpContext.HttpContext.Session.Get<AuthenticationResponse>("user");
            if (vm == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
