using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Improvement.Commands.CreateImprovement
{
    public class CreateImprovementCommand : IRequest<Response<BaseImprovementDto>>
    {
        [SwaggerParameter("The ID of the improvement.")]
        public int Id { get; set; }

        [SwaggerParameter("The type of improvement.")]
        public string ImprovementType { get; set; }

        [SwaggerParameter("The description of the improvement.")]
        public string Description { get; set; }

        public CreateImprovementCommand(int id, string improvementType, string description)
        {
            Id = id;
            ImprovementType = improvementType;
            Description = description;
        }
    }
    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, Response<BaseImprovementDto>>
    {
        private readonly IImprovementRepository _repository;
        private readonly IMapper _mapper;

        public CreateImprovementCommandHandler(IImprovementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<BaseImprovementDto>> Handle(CreateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _repository.AddAsync(_mapper.Map<Domain.Entities.Improvement>(command));
            return new Response<BaseImprovementDto>(_mapper.Map<BaseImprovementDto>(improvement));
        }
    }
}
