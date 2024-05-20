using AutoMapper;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.Domain
{
    public class SaleCategoryService : GenericService<SaveSaleCategoryViewModel, BaseSaleCategoryViewModel, SaleCategory>, ISaleCategoryService
    {
        private readonly ISaleCategoryRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public SaleCategoryService(ISaleCategoryRepository repository, IMapper mapper, IPropertyRepository propertyRepository) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        public override async Task<List<BaseSaleCategoryViewModel>> GetAllViewModel()
        {
            var listSalesCategories = await _repository.GetAllAsync();

            var listConvert = _mapper.Map<List<BaseSaleCategoryViewModel>>(listSalesCategories);

            if (listConvert.Count == 0)
            {
                return listConvert;
            }

            var properties = await _propertyRepository.GetAllAsync();

            foreach (var saleCategory in listConvert)
            {
                var propertiesCount = properties.Count(e => e.SaleCategory.Id == saleCategory.Id);
                saleCategory.PropertiesCount = propertiesCount;
            }

            return listConvert;
        }
    }
}