using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.PropertyType.Queries.GetAllPropertyTypes
{
    public record GetAllPropertyTypesQuery() : IRequest<Response<ICollection<BasePropertyTypeDto>>>;

    public class GetAllPropertyTypesQueryHandler : IRequestHandler<GetAllPropertyTypesQuery, Response<ICollection<BasePropertyTypeDto>>>
    {
        private readonly IPropertyTypeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPropertyTypesQueryHandler(IPropertyTypeRepository repository,  IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ICollection<BasePropertyTypeDto>>> Handle(GetAllPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync();
            if (list == null || list.Count == 0) throw new ApiException("No data found.", (int)HttpStatusCode.NoContent);
            var listConverted = list.Select(e => _mapper.Map<BasePropertyTypeDto>(e)).ToList();
            return new Response<ICollection<BasePropertyTypeDto>>(listConverted);
        }
    }
}
