using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Improvement.Commands.DeleteImprovement
{
    public class DeleteImprovementByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter("The ID of the improvement to delete.")]
        public int Id { get; set; }

        public DeleteImprovementByIdCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteImprovementCommandHandler : IRequestHandler<DeleteImprovementByIdCommand, Response<int>>
    {
        private readonly IImprovementRepository _repository;
        private readonly IMapper _mapper;

        public DeleteImprovementCommandHandler(IImprovementRepository repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteImprovementByIdCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _repository.GetEntityByIdAsync(command.Id);
            if (improvement == null) throw new Exception("Entity not found.");
            await _repository.DeleteAsync(improvement);
            return new Response<int>(improvement.Id);
        }
    }
}
