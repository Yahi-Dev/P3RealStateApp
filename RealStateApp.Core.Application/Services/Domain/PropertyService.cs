using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Interfaces.Services.Identity;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.Domain
{
    public class PropertyService : GenericService<SavePropertyViewModel, BasePropertyViewModel, Property>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropertyImprovementRepository _propertyImprovementRepository;
        private readonly AuthenticationResponse _userViewModel;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly ISaleCategoryRepository _saleCategoryRepository;

        public PropertyService(
            IPropertyImprovementRepository propertyImprovementRepository,
            IPropertyRepository propertyRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService,
            IClientFavoritePropertyRepository clientFavoritePropertyRepository,
            IPropertyTypeService propertyTypeService,
            IPropertyImageService propertyImageService,
            ISaleCategoryRepository saleCategoryRepository
            )
            : base(propertyRepository, mapper)
        {
            _propertyImprovementRepository = propertyImprovementRepository;
            _saleCategoryRepository = saleCategoryRepository;
            _propertyImageService = propertyImageService;
            _accountService = accountService;
            _propertyRepository = propertyRepository;
            _httpContextAccessor = httpContextAccessor;
            _userViewModel = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _mapper = mapper;
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
            _propertyTypeService = propertyTypeService;
        }

        public async Task<BasePropertyViewModel> GetPropertyByCode(FiltersPropertiesViewModel filters)
        {
            var properties = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Images", "Improvements" });
            var property = properties.FirstOrDefault(x => x.Code == filters.Code);
            return _mapper.Map<BasePropertyViewModel>(property);
        }

        public async Task<BasePropertyViewModel> GetByIdAsync(int Id)
        {
            var properties = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Images", "Improvements" });
            var property = properties.FirstOrDefault(x => x.Id == Id);
            return _mapper.Map<BasePropertyViewModel>(property);
        }

        public async Task<List<BasePropertyViewModel>> GetAllPropertyWithFilters(FiltersPropertiesViewModel filters)
        {
            List<BasePropertyViewModel> filteredProperties = await Filter(filters);
            return _mapper.Map<List<BasePropertyViewModel>>(filteredProperties);
        }

        public async Task<List<BasePropertyViewModel>> Filter(FiltersPropertiesViewModel filters)
        {
            var propertiesList = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Images", "Improvements" });

            propertiesList = propertiesList.Where(p =>
                (filters.Code == null || p.Code.Contains(filters.Code)) &&
                (!filters.PropertyTypeId.HasValue || p.PropertyTypeId == filters.PropertyTypeId.Value) &&
                (!filters.PriceMin.HasValue || p.Price >= filters.PriceMin.Value) &&
                (!filters.PriceMax.HasValue || p.Price <= filters.PriceMax.Value) &&
                (!filters.Bedrooms.HasValue || p.Bedrooms == filters.Bedrooms.Value) &&
                (!filters.Bathrooms.HasValue || p.Bathrooms == filters.Bathrooms.Value))
                .OrderByDescending(p => p.CreatedDate)
                .ToList();

            var propertiesVm = _mapper.Map<List<BasePropertyViewModel>>(propertiesList);
            var userViewModel = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");

            return propertiesVm;
        }

        public async Task<List<BasePropertyImageViewModel>> GetPropertyImagesById(int propertyId)
        {
            List<PropertyImage> images = await _propertyRepository.GetPropertyImagesById(propertyId);

            List<BasePropertyImageViewModel> imageViewModels = _mapper.Map<List<BasePropertyImageViewModel>>(images);

            return imageViewModels;
        }

        public async Task<PropertyInfoViewModel> GetInfoByIdViewModel(int id)
        {
            var properties = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Images", "Improvements" });

            var property = properties.First(e=>e.Id == id);

            PropertyInfoViewModel propertyInfo = new();

            List<BasePropertyImprovementViewModel> Improvements = new();

            var agent = await _accountService.GetByIdAsync(property.AgentId);
            propertyInfo.Agent = _mapper.Map<BaseUserViewModel>(agent);
            propertyInfo.Property = _mapper.Map<BasePropertyViewModel>(property);


            var AllImprovements = await _propertyImprovementRepository.GetAllAsync();

            var SelectImprovementsOfVm = AllImprovements.Where(x => x.PropertyId == id).ToList();

            propertyInfo.Property.Improvements = _mapper.Map<List<BasePropertyImprovementViewModel>>(SelectImprovementsOfVm);

            return propertyInfo;
        }

        public async Task<List<BasePropertyViewModel>> GetAllAgentProperty(string agentId)
        {
            var properties = await _propertyRepository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Images", "Improvements" });

            var propertyAgent = properties.Where(x => x.AgentId == agentId).ToList();


            var list = propertyAgent.Select(x => new BasePropertyViewModel
            {
                Id = x.Id,
                PropertyTypeId = x.PropertyTypeId,
                PropertyType = _mapper.Map<BasePropertyTypeViewModel>(x.PropertyType),
                Image = x.Images.FirstOrDefault(x => x.IsMain == true)?.ImageURL,
                Code = x.Code,
                SaleCategoryId = x.SaleCategoryId,
                SaleCategory = _mapper.Map<BaseSaleCategoryViewModel>(x.SaleCategory),
                Price = x.Price,
                Bathrooms = x.Bathrooms,
                Bedrooms = x.Bedrooms,
                Size = x.Size,
                Location = x.Location,
                AgentId = x.AgentId,
                Improvements = _mapper.Map<List<BasePropertyImprovementViewModel>>(x.Improvements)
            }).ToList();

            return list;
        }

        public override async Task<SavePropertyViewModel> Add(SavePropertyViewModel vm)
        {
            List<string> codes = new();
            var properties = await _propertyRepository.GetAllAsync();

            foreach (var propertyCode in properties)
            {
                codes.Add(propertyCode.Code);
            }

            Property propertyForAdd = _mapper.Map<Property>(vm);

            propertyForAdd.Code = CreateCodeByProperty.GetNewCode(codes);

            var property = await _propertyRepository.AddAsync(propertyForAdd);

            SavePropertyViewModel propertyVm = _mapper.Map<SavePropertyViewModel>(property);

            return propertyVm;
        }

        public async Task RemoveAllPropertyImprovements(int propertyId)
        {
            var propertyImprovements = await _propertyImprovementRepository.GetByPropertyId(propertyId);
            foreach (var propertyImprovement in propertyImprovements)
            {
                await _propertyImprovementRepository.DeleteAsync(propertyImprovement);
            }
        }

       public override async Task<List<BasePropertyViewModel>> GetAllViewModel()
       {
            var properties = await _propertyRepository.GetAllAsync();
            var propertyImage = _mapper.Map<List<PropertyImage>>(await _propertyImageService.GetAllViewModel());
            var saleCategories = await _saleCategoryRepository.GetAllAsync();


            foreach (var property in properties)
            {
                property.PropertyType = _mapper.Map<PropertyType>(await _propertyTypeService.GetByIdSaveViewModel(property.PropertyTypeId));
                property.Images = propertyImage.Where(x => x.PropertyId == property.Id && x.IsMain == true).ToList();
                property.SaleCategory = _mapper.Map<SaleCategory>( saleCategories.Find(x => x.Id == property.SaleCategoryId));
            }

            var propertiesVm  = _mapper.Map<List<BasePropertyViewModel>>(properties);

            return propertiesVm;
       }

    }
}
