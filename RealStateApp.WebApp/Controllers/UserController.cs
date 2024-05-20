using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.WebApp.Middelwares;
using System.Data;

namespace RealStateApp.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userservice;

        public UserController(IUserService userService)
        {
            _userservice = userService;
        }


        public async Task<IActionResult> LogOut()
        {
            await _userservice.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }


        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse uservm = await _userservice.LoginAsync(vm);

            if (uservm != null && uservm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", uservm);
                if (uservm.Role == RolesEnum.Agent.ToString())
                {
                    return RedirectToRoute(new { controller = "Agent", action = "Index" });
                }
                if (uservm.Role == RolesEnum.Admin.ToString())
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                }
                else
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }
            else
            {
                vm.HasError = uservm.HasError;
                vm.Error = uservm.Error;
                return View(vm);
            }
			return View();
		}

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }



        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];

            //Desarrollar la logica de guardar la imagen en la base de datos.
            var response = await _userservice.RegisterAsync(vm, origin);
            if (!response.Succeeded)
            {
                vm.HasError = !response.Succeeded;
                vm.Error = response.Message;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> ConfirmEmail(string UserId, string token)
        {
            string response = await _userservice.ConfirmEmailAsync(UserId, token);
            return View("ConfirmEmail", response);
        }



        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            var response = await _userservice.ForgotPasssowrdAsync(vm, origin);
            if (!response.Succeeded)
            {
                vm.HasError = !response.Succeeded;
                vm.Error = response.Message;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }


        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var response = await _userservice.ResetPasssowrdAsync(vm);
            if (!response.Succeeded)
            {
                vm.HasError = !response.Succeeded;
                vm.Error = response.Message;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }







        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
