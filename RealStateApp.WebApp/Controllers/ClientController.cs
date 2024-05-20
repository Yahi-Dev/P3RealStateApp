using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.Services.Domain;
using RealStateApp.Core.Application.ViewModels.Domain.ClientFavoriteProperty;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using System.Data;

namespace RealStateApp.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientFavoritePropertyService _clientFavoritePropertyService;
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IUserService _userService;


        public ClientController(
            IClientFavoritePropertyService clientFavoritePropertyService,
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            IUserService userService)
        {
            _userService = userService;
            _clientFavoritePropertyService = clientFavoritePropertyService;
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
        }

        public async Task<IActionResult> Index(FiltersPropertiesViewModel vm)
        {
            ViewBag.PropertyType = await _propertyTypeService.GetAllViewModel();
            return View(await _propertyService.GetAllPropertyWithFilters(vm));
        }
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> MyFavoriteProperty(string ClientId)
        {
            return View(await _clientFavoritePropertyService.GetAllFavoriteProperty(ClientId));
        }
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
