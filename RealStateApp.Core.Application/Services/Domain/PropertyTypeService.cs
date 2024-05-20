using AutoMapper;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.Domain
{
    public class PropertyTypeService : GenericService<SavePropertyTypeViewModel, BasePropertyTypeViewModel, PropertyType>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public PropertyTypeService(IPropertyTypeRepository repository, IMapper mapper, IPropertyRepository propertyRepository) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _propertyRepository = propertyRepository;
        }

        public override async Task<List<BasePropertyTypeViewModel>> GetAllViewModel()
        {
            var listPropertyTypes = await _repository.GetAllAsync();
            var listConvert = _mapper.Map<List<BasePropertyTypeViewModel>>(listPropertyTypes);
            var properties = await _propertyRepository.GetAllAsync();

            foreach (var propertyType in listConvert)
            {
                listConvert[listConvert.IndexOf(propertyType)].PropertiesCount = properties.Count(e => e.PropertyType.Id == propertyType.Id);
            }

            return listConvert;
        }
    }
}
