using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace RealStateApp.Core.Application.Services.Domain
{
    public class PropertyImageService : GenericService<SavePropertyImageViewModel, BasePropertyImageViewModel, PropertyImage>, IPropertyImageService
    {
        private IPropertyImageRepository _repository { get; set; }
        private IMapper _mapper { get; set; }
        private IHttpContextAccessor _httpContextAccessor;
        public PropertyImageService(IPropertyImageRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public override async Task Update(SavePropertyImageViewModel vm, int id)
        {
            vm.UserId = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id;

            await base.Update(vm, id);
        }

        
    }
}
