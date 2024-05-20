using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.ClientFavoriteProperty;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.Domain
{
    public class ClientFavoritePropertyService : GenericService<SaveClientFavoritePropertyViewModel, BaseClientFavoritePropertyViewModel, ClientFavoriteProperty>, IClientFavoritePropertyService
    {
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse _userViewModel;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IMapper _mapper;

        public ClientFavoritePropertyService(
            IClientFavoritePropertyRepository clientFavoritePropertyRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IPropertyRepository propertyRepository,
            IPropertyImageService propertyImageService)
            : base(clientFavoritePropertyRepository, mapper)
        {
            _propertyImageService = propertyImageService;
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        public async Task<List<BasePropertyViewModel>>GetAllFavoriteProperty(string ClientId)
        {
            List<BasePropertyViewModel> properties = new();
            var PropertyClient = await _clientFavoritePropertyRepository.GetAllAsync();
            var propertyId = PropertyClient.Where(x => x.ClientId == ClientId).ToList();
            var propertyImage = _mapper.Map<List<PropertyImage>>(await _propertyImageService.GetAllViewModel());

            foreach (var Idproperty in PropertyClient)
            {
                var property = await _propertyRepository.GetEntityByIdAsync(Idproperty.PropertyId);
                property.Images = propertyImage.Where(x => x.PropertyId == property.Id && x.IsMain == true).ToList();

                BasePropertyViewModel propertyVm = _mapper.Map<BasePropertyViewModel>(property);
                properties.Add(propertyVm);
            }




            return properties;
        } 
    }
}

