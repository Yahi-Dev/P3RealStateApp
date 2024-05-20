using RealStateApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using AutoMapper;
using RealStateApp.WebApp.Models.Agent;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using RealStateApp.Core.Application.Enums;

namespace RealStateApp.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _userViewModel;

        public AgentController(
            IAgentService agentService,
            IPropertyService propertyService,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _propertyService = propertyService;
            _agentService = agentService;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Index() 
        {
            return View(await _propertyService.GetAllAgentProperty(_userViewModel.Id));
        }


        public async Task<IActionResult> GetActiveAgents()
        {
            return View(await _agentService.GetAgentActiveList());
        }

        public async Task<IActionResult> GetPropertyByAgent(string agentId)
        {
            AgentPropertiesWithAgentViewModel vm = new();
            var result = await _agentService.GetByIdAsync(agentId);
            vm.Properties = await _propertyService.GetAllAgentProperty(agentId);
            vm.Agent = result.Data;
            return View(vm);
        }

        public async Task<IActionResult> Property(string agentId)
        {
            var Properties = await _propertyService.GetAllAgentProperty(agentId);
            return View(Properties);
        }

        public async Task<IActionResult> AgentDetails(string id)
        {
            var agent = await _agentService.GetByIdAsync(id);
            var properties = await _propertyService.GetAllAgentProperty(id);

            var detail = new AgentDetailsViewModel()
            {
                Agent = agent.Data,
                Properties = properties
            };

            return View(detail);
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> GetAgentByName(FilterFindUser user)
        {
            if (user.NameAgent == null)
            {
                return RedirectToRoute(new { controller = "Agent", action = "Index" });
            }
            return View(await _userService.GetAgentByFilter(user));
        }



        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> EditProfile()
        {
            var agent = _agentService.GetByIdAsync(_userViewModel.Id);
            SaveUserViewModel vm = _mapper.Map<SaveUserViewModel>(agent.Result.Data);
            return View(vm);
        }


        [Authorize(Roles = "Agent")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(SaveUserViewModel vm)
        {
            var agent = await _agentService.GetByIdAsync(vm.Id);
            SaveUserViewModel UserVm = _mapper.Map<SaveUserViewModel>(agent.Data);

            vm.UserName = UserVm.UserName;
            vm.Email = UserVm.Email;
            vm.IdCard = UserVm.IdCard;
            vm.IsActive = UserVm.IsActive;


            UserVm.Role = RolesEnum.Agent.ToString();
            if (!ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(vm.Password))
                {
                    vm.Role = "Agent";
                    vm.IdCard = UserVm.IdCard;
                    ModelState.Remove("Password");
                    ModelState.Remove("Role");
                    ModelState.Remove("IdentificationCard");
                }

            }
            if (!ModelState.IsValid)
            {
                if (vm.FirstName == null || vm.LastName == null || vm.PhoneNumber == null)
                {
                    return View("EditProfile", vm);
                }
                else
                {
                    string imagePath = string.IsNullOrEmpty(UserVm.ImageUrl) ? "" : UserVm.ImageUrl;
                    vm.ImageUrl = UploadImage.UploadFile(vm.formFile, vm.Id, true, imagePath);

                    await _agentService.UpdateAsync(vm, vm.Id);
                }
            }

            return RedirectToRoute(new { controller = "Agent", action = "Index" });
        }




        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Profile(string id)
        {
            var response = await _agentService.GetByIdAsync(id);
            BaseUserViewModel vm = response.Data;
            return View(vm);
        }

    }
}
