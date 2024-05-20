using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.Property.Commands.UpdateProperty
{
    public class UpdatePropertyCommand : IRequest<Response<UpdatePropertyResponse>>
    {
        [SwaggerParameter("The ID of the property.")]
        public int Id { get; set; }

        [SwaggerParameter("The code of the property.")]
        public string Code { get; set; }

        [SwaggerParameter("The ID of the property type.")]
        public int PropertyTypeId { get; set; }

        [SwaggerParameter("The ID of the sale category.")]
        public int SaleCategoryId { get; set; }

        [SwaggerParameter("The price of the property in DOP.")]
        public decimal Price { get; set; }

        [SwaggerParameter("The size of the property meters squared.")]
        public float Size { get; set; }

        [SwaggerParameter("The number of bedrooms in the property.")]
        public int Bedrooms { get; set; }

        [SwaggerParameter("The number of bathrooms in the property.")]
        public int Bathrooms { get; set; }

        [SwaggerParameter("The description of the property.")]
        public string Description { get; set; }

        [SwaggerParameter("The ID of the agent associated with the property.")]
        public string AgentId { get; set; }

        public UpdatePropertyCommand(int id, string code, int propertyTypeId, int saleCategoryId, decimal price, float size, int bedrooms, int bathrooms, string description, string agentId)
        {
            Id = id;
            Code = code;
            PropertyTypeId = propertyTypeId;
            SaleCategoryId = saleCategoryId;
            Price = price;
            Size = size;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Description = description;
            AgentId = agentId;
        }
    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, Response<UpdatePropertyResponse>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;
        public UpdatePropertyCommandHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<UpdatePropertyResponse>> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = await _repository.GetEntityByIdAsync(command.Id);

            if (property == null) throw new ApiException("Property not found", (int)HttpStatusCode.NoContent);

            property = _mapper.Map<Domain.Entities.Property>(command);

            await _repository.UpdateAsync(property, property.Id);

            var propertyResponse = _mapper.Map<UpdatePropertyResponse>(property);

            return new Response<UpdatePropertyResponse>(propertyResponse);
        }
    }
}
