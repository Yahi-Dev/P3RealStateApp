using Microsoft.AspNetCore.Mvc.Filters;
using RealStateApp.WebApp.Controllers;

namespace RealStateApp.WebApp.Middelwares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public LoginAuthorize(ValidateUserSession validateUser)
        {
            _userSession = validateUser;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var controller = (UserController)context.Controller;
                context.Result = controller.RedirectToAction("Index", "Home");
            }
            else
            {
                await next();
            }
        }
    }
}
