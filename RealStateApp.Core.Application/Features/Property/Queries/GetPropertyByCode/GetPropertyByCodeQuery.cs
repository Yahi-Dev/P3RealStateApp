using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Property.Queries.GetPropertyById;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Property.Queries.GetPropertyByCode
{
    public class GetPropertyByCodeQuery : IRequest<Response<BasePropertyDto>>
    {
        [SwaggerParameter("The code associated with the property.")]
        public string Code { get; set; }

        public GetPropertyByCodeQuery(string code)
        {
            Code = code;
        }
    }
    public class GetPropertyByCodeQueryHandler : IRequestHandler<GetPropertyByCodeQuery, Response<BasePropertyDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _repository;
        public GetPropertyByCodeQueryHandler(IMapper mapper, IPropertyRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<BasePropertyDto>> Handle(GetPropertyByCodeQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.GetAllWithIncludeAsync(new List<string> { "PropertyType", "SaleCategory", "Improvements", "Images" });
            
            var property = properties.FirstOrDefault(e => e.Code == request.Code);
            
            if (property == null) throw new ApiException("Code not found.", (int)HttpStatusCode.NotFound);
            
            return new Response<BasePropertyDto>(_mapper.Map<BasePropertyDto>(property));
        }
    }
}
