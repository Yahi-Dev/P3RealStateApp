using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Property.Commands.DeleteProperty
{
    public class DeletePropertyByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter("The ID of the property to delete.")]
        public int Id { get; set; }

        public DeletePropertyByIdCommand(int id)
        {
            Id = id;
        }
    }
    public class DeletePropertyByIdCommandHandler : IRequestHandler<DeletePropertyByIdCommand, Response<int>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;
        public DeletePropertyByIdCommandHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeletePropertyByIdCommand command, CancellationToken cancellationToken)
        {
            var property = await _repository.GetEntityByIdAsync(command.Id);

            if (property == null) throw new Exception("Property not found");

            await _repository.DeleteAsync(property);

            return new Response<int>(property.Id);
        }
    }
}
