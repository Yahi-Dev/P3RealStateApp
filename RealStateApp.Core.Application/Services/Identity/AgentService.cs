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
    public class AgentService : BaseIdentityUsersService, IAgentService
    {
        private readonly IAccountService _accountService;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _userViewModel;
        private readonly IPropertyRepository _propertyRepository;

        public AgentService(
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
        public async Task<List<AgentViewModel>> GetAgentList()
        {
            var userList = await GetAllAsync();
            var agentList = userList.Data.FindAll(e => e.Role == RolesEnum.Agent.ToString()).ToList();
            List<AgentViewModel> completeAgentList = new();
            List<BasePropertyViewModel> agentProperties = new();

            foreach (var agent in agentList)
            {
                try
                {
                    agentProperties = await _propertyService.GetAllAgentProperty(agent.Id);
                    completeAgentList.Add(new AgentViewModel
                    {
                        Id = agent.Id,
                        FirstName = agent.FirstName,
                        LastName = agent.LastName,
                        Email = agent.Email,
                        IsActive = agent.IsActive,
                        Role = agent.Role,
                        QuantityProperty = agentProperties.Count()
                    });
                }
                catch(Exception ex)
                {
                    completeAgentList.Add(new AgentViewModel
                    {
                        Id = agent.Id,
                        FirstName = agent.FirstName,
                        LastName = agent.LastName,
                        Email = agent.Email,
                        Role = agent.Role,
                        IsActive = agent.IsActive,
                        QuantityProperty = 0
                    });
                }
            }
            return completeAgentList;
        }

        public async Task<List<AgentMainMenuViewmodel>> GetAgentActiveList()
        {
            var userList = await GetAllAsync();
            var agentList = userList.Data.FindAll(e => e.Role == RolesEnum.Agent.ToString() && e.IsActive == true).ToList();

            List<AgentMainMenuViewmodel> completeAgentList = new();
            List<BasePropertyViewModel> agentProperties = new();

            foreach (var agent in agentList)
            {
                //agentProperties = await _propertyService.GetAllAgentProperty(agent.Id);

                completeAgentList.Add(new AgentMainMenuViewmodel
                {
                    AgentId = agent.Id,
                    FirstName = agent.FirstName,
                    LastName = agent.LastName,
                    ImageUrl = agent.ImageUrl
                });
            }
            return completeAgentList;
        }


        public async Task<bool> DeleteUserAsync(string userId)
        {

            var agentProperties = await _propertyService.GetAllAgentProperty(userId);

            foreach (var property in agentProperties)
            {
                await _propertyService.Delete(property.Id);
            }
            var agent = await _accountService.GetByIdAsync(userId);
            await _accountService.Remove(agent);

            return true;
        }
    }
}
