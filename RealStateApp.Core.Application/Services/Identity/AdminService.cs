using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Services.Identity
{
    public class AdminService : BaseIdentityUsersService, IAdminService
    {
        private readonly IAccountService _accountService;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _userViewModel;
        private readonly IPropertyRepository _propertyRepository;

        public AdminService(
            IAccountService accountService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
             IPropertyRepository propertyRepository,
             IPropertyService propertyService) : base(accountService, mapper)
        {
            _propertyService = propertyService;
            _propertyRepository = propertyRepository;
            _accountService = accountService;
            _mapper = mapper;
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<DashboardViewModel> GetDashboardAdmin()
        {
            var properties = await _propertyRepository.GetAllAsync();
            var users = await _accountService.GetAllUsers();

            DashboardViewModel vm = new();

            vm.RegisterPropertys = properties.Count();

            vm.ActivesAngents = users.Where(x => x.Role == RolesEnum.Agent.ToString() && x.IsActive == true).Count();
            vm.InactivesAgents = users.Where(x => x.Role == RolesEnum.Agent.ToString() && x.IsActive == false).Count();

            vm.ActivesClients = users.Where(x => x.Role == RolesEnum.Client.ToString() && x.IsActive == true).Count();
            vm.InactivesClients = users.Where(x => x.Role == RolesEnum.Client.ToString() && x.IsActive == false).Count();

            vm.ActivesDevelopers = users.Where(x => x.Role == RolesEnum.Developer.ToString() && x.IsActive == true).Count();
            vm.InactivesDevelopers = users.Where(x => x.Role == RolesEnum.Developer.ToString() && x.IsActive == false).Count();

            return vm;
        }

        public async override Task<Response<List<BaseUserViewModel>>> GetAllAsync()
        {
            var result = await base.GetAllAsync();
            result.Data = result.Data.FindAll(e => e.Role == RolesEnum.Admin.ToString());
            return result;
        }

        public async Task<List<BaseUserViewModel>> GetAllAdminList()
        {
            List<BaseUserViewModel> vm = new();

            var userList = await _accountService.GetAllUsers();
            var filteredUsers = userList.Where(x => x.Email != _userViewModel.Email && x.Role == RolesEnum.Admin.ToString()).ToList();

            foreach (var admin in filteredUsers)
            {
                //agentProperties = await _propertyService.GetAllAgentProperty(agent.Id);

                vm.Add(new BaseUserViewModel
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.Email,
                    UserName = admin.UserName,
                    Role = admin.Role,
                    IdCard = admin.IdCard,
                });
            }
            return vm;
        }



    }
}
