using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.Services.Domain;
using RealStateApp.Core.Application.Services.Identity;
using RealStateApp.Core.Application.ViewModels.Domain.Improvement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Data;

namespace RealStateApp.WebApp.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
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


        public AdminController(
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


        #region General
        public async Task<IActionResult> Index()
        {
            return View(await _adminService.GetDashboardAdmin());
        }

        public async Task<IActionResult> ChangeAgentStatus(string id, string role)
        {
            SaveChangeStatusViewModel vm = new();
            vm.Id = id;
            vm.Role = role;

            var error = await _baseIdentityUsersService.ChangeStatusAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "ListAgent" });
        }

        #endregion



        #region Agents
        public async Task<IActionResult> ListAgent()
        {
            return View(await _agentService.GetAgentList());
        }

        public async Task<IActionResult> DeleteAgent(string AgentId)
        {
            return View(await _accountService.GetByIdAsync(AgentId));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteAgent(string id, string o = null)
        {
            var agent = await _accountService.GetByIdAsync(id);
            await _accountService.Remove(agent);
            return RedirectToRoute(new { controller = "Admin", action = "Agents" });
        }



        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }



        #endregion


        #region Admins
        public async Task<IActionResult> AdminMaintenance()
        {
            ViewData["Title"] = "Mantenimiento de Administradores";
            var list = await _adminService.GetAllAdminList();
            List<BaseUserViewModel> users = list;
            return View(users);
        }

        public IActionResult CreateAdmin()
        {
            SaveAdminViewModel userVm = new()
            {
                Role = RolesEnum.Admin.ToString()
            };

            return View(userVm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(SaveAdminViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var userVm = _mapper.Map<SaveUserViewModel>(vm);
            userVm.Role = RolesEnum.Admin.ToString();
            userVm.PhoneNumber = "-";
            var origin = Request.Headers["origin"];
            
            Core.Application.Wrappers.Response<int> response = await _userService.RegisterAsync(userVm, origin);

            if (!response.Succeeded)
            {
                vm.HasError = !response.Succeeded;
                vm.Error = response.Message;
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

        [HttpPost]
        public async Task<IActionResult> EditAdmin(SaveUserViewModel vm)
        {
            var result = await _adminService.GetByIdAsync(vm.Id);
            SaveUserViewModel UserVm = _mapper.Map<SaveUserViewModel>(result.Data);

            if (!ModelState.IsValid && vm.PhoneNumber != null && vm.formFile != null)
            {
                if (string.IsNullOrEmpty(vm.Password) && string.IsNullOrEmpty(vm.ConfirmPassword))
                {
                    vm.Password = UserVm.Password;
                    vm.ConfirmPassword = UserVm.ConfirmPassword;
                    ModelState.Remove("Password");
                    ModelState.Remove("ConfirmPassword");
                }
                if (string.IsNullOrEmpty(vm.PhoneNumber))
                {
                    vm.PhoneNumber = UserVm.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }
            }
            if (!ModelState.IsValid && vm.FirstName == null && vm.LastName == null && vm.UserName == null && vm.Password == null 
                && vm.Email == null && vm.IdCard == null)
            {
                return View("EditAdmin", vm);
            }
            vm.IsActive = UserVm.IsActive;
            await _adminService.UpdateAsync(vm, vm.Id);
            TempData["UserSucceed"] = "Usuario editado exitosamente.";
            return RedirectToAction("AdminMaintenance", new { role = vm.Role });
        }
        #endregion

        #region Developers

        public async Task<IActionResult> DeveloperMaintenance()
        {
            ViewData["Title"] = "Mantenimiento de Desarrolladores";
            var list = await _developerService.GetListDeveloper();
            List<BaseUserViewModel> users = list;
            return View(users);
        }

        public IActionResult CreateDeveloper()
        {
            SaveUserViewModel userVm = new()
            {
                Role = RolesEnum.Developer.ToString()
            };

            return View(userVm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeveloper(SaveUserViewModel vm)
        {
            vm.Role = RolesEnum.Developer.ToString();
            if (!ModelState.IsValid )
            {
                if(string.IsNullOrEmpty(vm.Password))
                {
                    vm.Role = "Developer";
                    ModelState.Remove("PhoneNumber");
                    ModelState.Remove("Role");
                    ModelState.Remove("IdentificationCard");
                }
                return View(vm);
            }
            var origin = Request.Headers["origin"];

            Core.Application.Wrappers.Response<int> response = await _userService.RegisterAsync(vm, origin);

            if (!response.Succeeded)
            {
                vm.HasError = !response.Succeeded;
                vm.Error = response.Errors.FirstOrDefault();
                return View(vm);
            }
            TempData["UserSucceed"] = "Usuario agregado exitosamente.";
            return RedirectToRoute(new { controller = "Admin", action = "DeveloperMaintenance" });
        }

        public async Task<IActionResult> EditDeveloper(string id)
        {
            var result = await _developerService.GetByIdAsync(id);
            SaveUserViewModel vm = _mapper.Map<SaveUserViewModel>(result.Data);
            vm.Role = RolesEnum.Developer.ToString();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeveloper(SaveUserViewModel vm)
        {
            var result = await _developerService.GetByIdAsync(vm.Id);
            SaveUserViewModel UserVm = _mapper.Map<SaveUserViewModel>(result);

            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(vm.Password) && string.IsNullOrEmpty(vm.ConfirmPassword))
                {
                    vm.Password = UserVm.Password;
                    vm.ConfirmPassword = UserVm.ConfirmPassword;
                    ModelState.Remove("Password");
                    ModelState.Remove("ConfirmPassword");
                }
                if (string.IsNullOrEmpty(vm.PhoneNumber))
                {
                    vm.PhoneNumber = UserVm.PhoneNumber;
                    ModelState.Remove("PhoneNumber");
                }
            }
            if (!ModelState.IsValid && vm.FirstName == null && vm.LastName == null && vm.UserName == null && vm.Password == null
                && vm.Email == null && vm.IdCard == null)
            {
                return View("EditDeveloper", vm);
            }
            vm.IsActive = UserVm.IsActive;
            await _developerService.UpdateAsync(vm, vm.Id);
            TempData["UserSucceed"] = "Usuario editado exitosamente.";
            return RedirectToAction("DeveloperMaintenance");
        }
            #endregion

        #region PropertyType
        // Mantenimiento de Tipos de Propiedad (ToP - Type of Property)
        public async Task<IActionResult> ListPropertyType()
        {
            return View(await _propertyTypeService.GetAllViewModel());
        }

        public async Task<IActionResult> CreatePropertyType()
        {
            SavePropertyTypeViewModel vm = new();
            return View("SavePropertyType", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropertyType(SavePropertyTypeViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Algo inesperado ocurrió al agregar el tipo de propiedad..";
                return View("SavePropertyType",vm);
            }

            await _propertyTypeService.Add(vm);
            TempData["PropertyTypeSucceed"] = "Tipo de Propiedad agregado exitosamente.";
            return RedirectToAction("ListPropertyType");
        }

        public async Task<IActionResult> EditPropertyType(int id)
        {
            SavePropertyTypeViewModel vm = await _propertyTypeService.GetByIdSaveViewModel(id);
            return View("SavePropertyType", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditPropertyType(SavePropertyTypeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Ocurrió un error editando el tipo de propiedad.";
                return RedirectToAction("ListPropertyType");
            }

            await _propertyTypeService.Update(vm, vm.Id);
            TempData["ToPSucceed"] = "Tipo de Propiedad editado exitosamente.";
            return RedirectToAction("ListPropertyType");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePropertyType(int id)
        {
            //ARRRRRREGLLLLLLLLLLLLLLLAAAAAAAAAAAAAAAARRRRRRRRRRRRR
            await _propertyTypeService.Delete(id);
            TempData["ToPSucceed"] = "Tipo de Propiedad eliminado exitosamente.";
            return RedirectToAction("ListPropertyType");
        }
        #endregion


        #region SaleCategory
        // Mantenimiento de Tipos de Ventas (ToS - Type of Sale)
        public async Task<IActionResult> ListSaleCategory()
        {
            var listSaleCategory = await _saleCategoryService.GetAllViewModel();
            return View(listSaleCategory);
        }

        public async Task<IActionResult> CreateSaleCategor()
        {
            SaveSaleCategoryViewModel vm = new();
            return View("SaveSaleCategory", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleCategory(SaveSaleCategoryViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Ocurrió un error agregando el tipo de venta.";
                return View("SaveSaleCategory",vm);
            }

            await _saleCategoryService.Add(vm);
            TempData["ToSSucceed"] = "Tipo de Venta agregado exitosamente.";
            return RedirectToAction("ListSaleCategory");
        }


        public async Task<IActionResult> EditSaleCategor(int id)
        {
            SaveSaleCategoryViewModel vm = await _saleCategoryService.GetByIdSaveViewModel(id);
            return View("SaveSaleCategory", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditSaleCategory(SaveSaleCategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Ocurrió un error editando el tipo de venta.";
                return RedirectToAction("ListSaleCategory");
            }

            await _saleCategoryService.Update(vm, vm.Id);
            TempData["SaleCategorSucceed"] = "Tipo de Venta editado exitosamente.";
            return RedirectToAction("ListSaleCategory");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSaleCategory(int id)
        {

            await _saleCategoryService.Delete(id);
            TempData["SaleCategorSucceed"] = "Tipo de Venta eliminado exitosamente.";
            return RedirectToAction("ListSaleCategory");
        }

        #endregion


        #region Mantenimiento de Mejoras
        // Mantenimiento de Mejoras (Improvements)
        public async Task<IActionResult> ListImprovements()
        {
            return View(await _improvementService.GetAllViewModel());
        }

        public async Task<IActionResult> CreateImprovement()
        {
            SaveImprovementViewModel vm = new();
            return View("SaveImprovement", vm);
        }


        [HttpPost]
        public async Task<IActionResult> CreateImprovement(SaveImprovementViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Ocurrió un error agregando la mejora.";
                return View("SaveImprovement", vm);
            }

            await _improvementService.Add(vm);
            TempData["ImprovementSucceed"] = "Mejora agregada exitosamente.";
            return RedirectToAction("ListImprovements");
        }

        public async Task<IActionResult> EditImprovement(int id)
        {
            SaveImprovementViewModel vm = await _improvementService.GetByIdSaveViewModel(id);
            return View("SaveImprovement", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditImprovement(SaveImprovementViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Ocurrió un error editando la mejora.";
                return RedirectToAction("ListImprovements");
            }

            await _improvementService.Update(vm, vm.Id);
            TempData["ImprovementSucceed"] = "Mejora editada exitosamente.";
            return RedirectToAction("ListImprovements");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImprovement(int id)
        {

            await _improvementService.Delete(id);
            TempData["ImprovementSucceed"] = "Mejora eliminada exitosamente.";
            return RedirectToAction("ListImprovements");
        }
        #endregion

    }
}

