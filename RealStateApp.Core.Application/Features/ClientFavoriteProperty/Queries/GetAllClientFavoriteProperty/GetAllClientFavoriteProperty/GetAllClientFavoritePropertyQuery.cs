using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.ClientFavoriteProperty.Queries.GetAllClientFavoriteProperty.GetAllClientFavoriteProperty
{
    /// <summary>
    /// Parametros para mostrar todos las propiedades favoritas de los clientes
    /// </summary>
    public class GetAllClientFavoritePropertyQuery : IRequest<Response<IEnumerable<BaseClientFavoriteProperty>>>
    {
    }

    public class GetAllClientFavoritePropertyQueryHandler : IRequestHandler<GetAllClientFavoritePropertyQuery, Response<IEnumerable<BaseClientFavoriteProperty>>>
    {
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;
        private readonly IMapper _mapper;

        public GetAllClientFavoritePropertyQueryHandler(IClientFavoritePropertyRepository clientFavoritePropertyRepository, IMapper mapper)
        {
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<BaseClientFavoriteProperty>>> Handle(GetAllClientFavoritePropertyQuery request, CancellationToken cancellationToken)
        {
            var clientFavoriteProperties = await _clientFavoritePropertyRepository.GetAllAsync();
            return new Response<IEnumerable<BaseClientFavoriteProperty>>(_mapper.Map<IEnumerable<BaseClientFavoriteProperty>>(clientFavoriteProperties));
        }
    }
}
