using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.PropertyType.Queries.GetPropertyTypeById
{
    public class GetPropertyTypeByIdQuery : IRequest<Response<BasePropertyTypeDto>>
    {
        [SwaggerParameter("The ID of the property type.")]
        public int Id { get; set; }

        public GetPropertyTypeByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetPropertyTypeByIdQueryHandler : IRequestHandler<GetPropertyTypeByIdQuery, Response<BasePropertyTypeDto>>
    {
        private readonly IPropertyTypeRepository _repository;
        private readonly IMapper _mapper;

        public GetPropertyTypeByIdQueryHandler(IPropertyTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<BasePropertyTypeDto>> Handle(GetPropertyTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var propertyType = await _repository.GetEntityByIdAsync(query.Id);
            if (propertyType == null) throw new Exception("Not found.");
            return new Response<BasePropertyTypeDto>(_mapper.Map<BasePropertyTypeDto>(propertyType));
        }
    }
}
