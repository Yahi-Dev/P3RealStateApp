using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.Helpers;

using RealStateApp.WebApp.Models;
using RealStateApp.WebApp.Models.Home;
using System.Diagnostics;

namespace RealStateApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IClientFavoritePropertyService _clientFavoritePropertyService;
        private IHttpContextAccessor _httpContextAccessor;

        public HomeController(
            IHttpContextAccessor httpContextAccessor,
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            IClientFavoritePropertyService clientFavoritePropertyService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _clientFavoritePropertyService = clientFavoritePropertyService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index(HomeViewModel home = null)
        {
            if (home.Filter == null)
                home.PropertyList = await _propertyService.GetAllViewModel();
            else
                home.PropertyList = await _propertyService.GetAllPropertyWithFilters(home.Filter);
            if(_httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user") != null)
                home.Favorites = await _clientFavoritePropertyService.GetAllFavoriteProperty(_httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id);
            var result = await _propertyTypeService.GetAllViewModel();
            home.PropertyTypes = result.Distinct().ToList();
            return View(home);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
