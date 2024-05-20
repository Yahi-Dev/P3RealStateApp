using AutoMapper;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.Domain
{
    public class PropertyImprovementService : GenericService<SavePropertyImprovementViewModel, BasePropertyImprovementViewModel, PropertyImprovement>, IPropertyImprovementService
    {
        private IPropertyImprovementRepository _repository;
        public PropertyImprovementService(IPropertyImprovementRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }
        public async Task AddPropertyImprovements(int propertyId, int improvementId)
        {
            var model = new PropertyImprovement()
            {
                PropertyId = propertyId,
                ImprovementId = improvementId
            };

            await _repository.AddAsync(model);
        }



    }
}
