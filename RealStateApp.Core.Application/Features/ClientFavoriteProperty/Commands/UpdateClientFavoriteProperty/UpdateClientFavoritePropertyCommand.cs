using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.ClientFavoriteProperty.Commands.UpdateClientFavoriteProperty
{
    /// <summary>
    /// Parametros para la actualizacion de la propiedad favorita de un cliente
    /// </summary>
    public class UpdateClientFavoritePropertyCommand : IRequest<Response<string>>
    {
        [SwaggerParameter(Description = "El Id del producto que se esta actualizando")]
        public int PropertyId { get; set; }

        [SwaggerParameter(Description = "El id del cliente al que se le editara")]
        public string ClientId { get; set; }
    }

    public class UpdateClientFavoritePropertyCommandhandler : IRequestHandler<UpdateClientFavoritePropertyCommand, Response<string>>
    {
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;
        private readonly IMapper _mapper;

        public UpdateClientFavoritePropertyCommandhandler(IClientFavoritePropertyRepository clientFavoritePropertyRepository, IMapper mapper)
        {
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateClientFavoritePropertyCommand command, CancellationToken cancellationToken)
        {
            return new Response<string>("Este metodo no esta implementado.");
        }
    }
}
