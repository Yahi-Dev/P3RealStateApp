using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;

namespace RealStateApp.WebApp.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IDeveloperService _developerService;
        private readonly IAccountService _accountService;
        private readonly IAgentService _agentService;
        private readonly IUserService _userService;
        private readonly IBaseIdentityUsersService _baseIdentityUsersService;
        private readonly IMapper _mapper;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ISaleCategoryService _saleCategoryService;
        private readonly IImprovementService _improvementService;


        public DeveloperController(
            IAdminService adminService,
            IAgentService agentService,
            IBaseIdentityUsersService baseIdentityUsersService,
            IAccountService accountService,
            IUserService userService, IMapper mapper,
            IPropertyTypeService propertyTypeService,
            ISaleCategoryService saleCategoryService,
            IImprovementService improvementService,
            IDeveloperService developerService)
        {
            _developerService = developerService;
            _accountService = accountService;
            _baseIdentityUsersService = baseIdentityUsersService;
            _agentService = agentService;
            _adminService = adminService;
            _userService = userService;
            _mapper = mapper;
            _propertyTypeService = propertyTypeService;
            _saleCategoryService = saleCategoryService;
            _improvementService = improvementService;
        }
        public IActionResult CreateDeveloper()
        {
            SaveDeveloperViewModel userVm = new()
            {
                Role = RolesEnum.Developer.ToString()
            };

            return View(userVm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeveloper(SaveDeveloperViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            vm.Role = RolesEnum.Admin.ToString();
            Core.Application.Wrappers.Response<int> response = await _userService.RegisterAsync(_mapper.Map<SaveUserViewModel>(vm), origin);

            if (!response.Succeeded)
            {
                vm.HasError = !response.Succeeded;
                vm.Error = response.Errors.FirstOrDefault();
                return View(vm);
            }
            TempData["UserSucceed"] = "Usuario agregado exitosamente.";
            return RedirectToRoute(new { controller = "Admin", action = "AdminMaintenance" });
        }

        public async Task<IActionResult> EditAdmin(string id)
        {
            var result = await _adminService.GetByIdAsync(id);
            var vm = _mapper.Map<SaveUserViewModel>(result.Data);
            vm.Role = RolesEnum.Admin.ToString();
            return View(vm);
        }
    }
}
