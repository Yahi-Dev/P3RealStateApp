using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Interfaces.Services.Identity.User;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;

namespace RealStateApp.Core.Application.Services.Identity
{
    public class DeveloperService : BaseIdentityUsersService, IDeveloperService
    {
        private readonly AuthenticationResponse _userViewModel;
        private readonly IMapper _mapper;
        public DeveloperService(
            IAccountService accountService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(accountService, mapper)
        {
            _mapper = mapper;
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<BaseUserViewModel>> GetListDeveloper()
        {
            List<BasePropertyViewModel> agentProperties = new();

            var developers = await GetAllAsync();

            var developersFilters = developers.Data.Where(x => x.Role == RolesEnum.Developer.ToString()).ToList();


            var developerList = developersFilters.Select(developer => new DeveloperViewModel
            {
                FirstName = developer.FirstName,
                LastName = developer.LastName,
                UserName = developer.UserName,
                IdCard = developer.IdCard,
                Email = developer.Email,
                IsActive = developer.IsActive,
                Id = developer.Id,
                Role = developer.Role,
                
            }).ToList();

            var developersVm = _mapper.Map<List<BaseUserViewModel>>(developerList);

            return developersVm;
        }
    }
}
