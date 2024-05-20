using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.Property.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<Response<int>>
    {
        [SwaggerParameter("The code of the property.")]
        public string Code { get; set; }

        [SwaggerParameter("The ID of the property type.")]
        public int PropertyTypeId { get; set; }

        [SwaggerParameter("The ID of the sale category.")]
        public int SaleCategoryId { get; set; }

        [SwaggerParameter("The price of the property in DOP.")]
        public decimal Price { get; set; }

        [SwaggerParameter("The size of the property in meters squared.")]
        public float Size { get; set; }

        [SwaggerParameter("The number of bedrooms in the property.")]
        public int Bedrooms { get; set; }

        [SwaggerParameter("The number of bathrooms in the property.")]
        public int Bathrooms { get; set; }

        [SwaggerParameter("The description of the property.")]
        public string Description { get; set; }

        [SwaggerParameter("The ID of the agent associated with the property.")]
        public string AgentId { get; set; }

        public CreatePropertyCommand(string code, int propertyTypeId, int saleCategoryId, decimal price, float size, int bedrooms, int bathrooms, string description, string agentId)
        {
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
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, Response<int>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;
        public CreatePropertyCommandHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreatePropertyCommand command, CancellationToken cancellationToken)
        {
            var result = await _repository.AddAsync(_mapper.Map<Domain.Entities.Property>(command));
            return new Response<int>(result.Id);
        }
    }
}
