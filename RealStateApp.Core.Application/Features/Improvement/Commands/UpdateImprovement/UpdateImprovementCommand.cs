using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.Improvement.Commands.UpdateImprovement
{
    public class UpdateImprovementCommand : IRequest<Response<UpdateImprovementResponse>>
    {
        [SwaggerParameter("The ID of the improvement.")]
        public int Id { get; set; }

        [SwaggerParameter("The type of improvement.")]
        public string ImprovementType { get; set; }

        [SwaggerParameter("The description of the improvement.")]
        public string Description { get; set; }

        public UpdateImprovementCommand(int id, string improvementType, string description)
        {
            Id = id;
            ImprovementType = improvementType;
            Description = description;
        }
    }
    public class UpdateImprovementCommandHandler : IRequestHandler<UpdateImprovementCommand, Response<UpdateImprovementResponse>>
    {
        private readonly IImprovementRepository _repository;
        private readonly IMapper _mapper;

        public UpdateImprovementCommandHandler(IImprovementRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateImprovementResponse>> Handle(UpdateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _repository.GetEntityByIdAsync(command.Id);
            
            if (improvement == null) throw new ApiException("Entity not found.",(int)HttpStatusCode.BadRequest);
            
            await _repository.UpdateAsync(_mapper.Map<Domain.Entities.Improvement>(command), command.Id);
            
            improvement = await _repository.GetEntityByIdAsync(command.Id);
            
            return new Response<UpdateImprovementResponse>(_mapper.Map<UpdateImprovementResponse>(improvement));

        }
    }
}
