using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Features.Property.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<Response<List<BasePropertyDto>>>
    {
    }
    public class GetAllPropertiesHandler : IRequestHandler<GetAllPropertiesQuery, Response<List<BasePropertyDto>>>
    {
        private readonly IPropertyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPropertiesHandler(IPropertyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<List<BasePropertyDto>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Improvements", "Images" });

            if (properties == null || properties.Count == 0)
            {
                throw new ApiException("No properties registered.", (int)HttpStatusCode.NoContent);
            }

            var propertiesConverted = new List<BasePropertyDto>();

            foreach (var property in properties)
            {
                var propertyDto = _mapper.Map<BasePropertyDto>(property);
                propertiesConverted.Add(propertyDto);
            }

            return new Response<List<BasePropertyDto>>(propertiesConverted);
        }
    }
}
