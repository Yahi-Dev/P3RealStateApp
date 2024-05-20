using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommand : IRequest<Response<UpdatePropertyTypeResponse>>
    {
        [SwaggerParameter("The ID of the property type.")]
        public int Id { get; set; }

        [SwaggerParameter("The updated name of the property type.")]
        public string Name { get; set; }

        [SwaggerParameter("The updated description of the property type.")]
        public string Description { get; set; }

        public UpdatePropertyTypeCommand(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, Response<UpdatePropertyTypeResponse>>
    {
        private readonly IPropertyTypeRepository _repository;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<UpdatePropertyTypeResponse>> Handle(UpdatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var propertyType = await _repository.GetEntityByIdAsync(command.Id);

            if (propertyType == null) throw new ApiException("Id not found.", (int)HttpStatusCode.BadRequest);

            propertyType = _mapper.Map<Domain.Entities.PropertyType>(command);

            await _repository.UpdateAsync(propertyType, propertyType.Id);

            propertyType = await _repository.GetEntityByIdAsync(command.Id);

            return new Response<UpdatePropertyTypeResponse>(_mapper.Map<UpdatePropertyTypeResponse>(propertyType));
        }
    }
}
