using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.Property.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<Response<BasePropertyDto>>
    {
        [SwaggerParameter(Description ="The ID of the property.")]
        public int Id { get; set; }

        public GetPropertyByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetPropertyByIdHandler : IRequestHandler<GetPropertyByIdQuery, Response<BasePropertyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _repository;
        public GetPropertyByIdHandler(IMapper mapper, IPropertyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<BasePropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Improvements", "Images" });
            var property = properties.FirstOrDefault(e => e.Id == request.Id);
            if (property == null) throw new ApiException("Property not found.", (int)HttpStatusCode.NotFound);
            return new Response<BasePropertyDto>(_mapper.Map<BasePropertyDto>(property));
        }
    }
}
