using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.DeletePropertyType
{
    public class DeletePropertyTypeByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter("The ID of the property type to delete.")]
        public int Id { get; set; }

        public DeletePropertyTypeByIdCommand(int id)
        {
            Id = id;
        }
    }
    public class DeletePropertyTypeByIdCommandHandler : IRequestHandler<DeletePropertyTypeByIdCommand, Response<int>>
    {
        private readonly IPropertyTypeRepository _repository;
        private readonly IMapper _mapper;

        public DeletePropertyTypeByIdCommandHandler(IPropertyTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeletePropertyTypeByIdCommand command, CancellationToken cancellationToken)
        {
            var propertyType = await _repository.GetEntityByIdAsync(command.Id);
            
            if (propertyType == null) throw new ApiException("Id isn't registered.", (int)HttpStatusCode.BadRequest);
            
            await _repository.DeleteAsync(propertyType);
            
            return new Response<int>(propertyType.Id);
        }
    }
}
