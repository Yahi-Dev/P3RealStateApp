using AutoMapper;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services.Domain;
using RealStateApp.Core.Application.Services.Base;
using RealStateApp.Core.Application.ViewModels.Domain.Improvement;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Services.Domain
{
    public class ImprovementService : GenericService<SaveImprovementViewModel, BaseImprovementViewModel, Improvement>, IImprovementService
    {
        private IImprovementRepository _repository;
        public ImprovementService(IImprovementRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }
    }
}
