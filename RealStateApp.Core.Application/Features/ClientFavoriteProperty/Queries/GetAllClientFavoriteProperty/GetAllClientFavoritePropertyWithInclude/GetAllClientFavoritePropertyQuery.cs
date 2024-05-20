using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.ClientFavoriteProperty.Queries.GetAllClientFavoriteProperty.GetAllClientFavoritePropertyWithInclude
{
    /// <summary>
    /// Parametros Para filtrar las propiedades
    /// </summary>
    public class GetAllClientFavoritePropertyQuery : IRequest<Response<IList<Domain.Entities.ClientFavoriteProperty>>>
    {
        [SwaggerParameter(Description = "El Id de de la propiedad por la cual se va a filtrar")]
        public int? PropertyId { get; set; }
    }


    public class GetAllClientFavoritePropertyQueryHandler : IRequestHandler<GetAllClientFavoritePropertyQuery, Response<IList<Domain.Entities.ClientFavoriteProperty>>>
    {
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;
        private readonly IMapper _mapper;

        public GetAllClientFavoritePropertyQueryHandler(IClientFavoritePropertyRepository clientFavoritePropertyRepository, IMapper mapper)
        {
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
            _mapper = mapper;
        }

        public async Task<Response<IList<Domain.Entities.ClientFavoriteProperty>>> Handle(GetAllClientFavoritePropertyQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllClientFavoritePropertyParamerts>(request);
            var clientFavoritePropertyList = await GetAllViewModelWithFilters(filter);
            if (clientFavoritePropertyList == null || clientFavoritePropertyList.Count == 0) throw new ApiException("Properties not found", (int)HttpStatusCode.NotFound);
            return new Response<IList<Domain.Entities.ClientFavoriteProperty>>(clientFavoritePropertyList);
        }

        private async Task<List<Domain.Entities.ClientFavoriteProperty>> GetAllViewModelWithFilters(GetAllClientFavoritePropertyParamerts filters)
        {
            var clientFavoritePropertyList = await _clientFavoritePropertyRepository.GetAllWithIncludeAsync(new List<string> { "Property" });

            var listViewModels = clientFavoritePropertyList.Select(property => new Domain.Entities.ClientFavoriteProperty
            {
                Id = property.Id,
                ClientId = property.ClientId,
                PropertyId = property.PropertyId,
            }).ToList();

            if (filters.PropertyId != null)
            {
                listViewModels = listViewModels.Where(property => property.PropertyId == filters.PropertyId.Value).ToList();
            }

            return listViewModels;
        }
    }
}
