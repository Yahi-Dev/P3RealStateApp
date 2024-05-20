using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.ClientFavoriteProperty.Commands.CreateClientFavoriteProperty
{
    /// <summary>
    /// Parametos para el cliente marcar una propiedad como favorita
    /// </summary>
    public class CreateClientFavoritePropertyCommand : IRequest<Response<int>>
    {
        /// <example>
        /// PS5
        /// </example>
        [SwaggerParameter(Description = "El id de la propiedad")]
        public int PropertyId { get; set; }

        [SwaggerParameter(Description = "El id del cliente al cual se le va a marcar la propiedad como favorita")]
        public string ClientId { get; set; }
    }

    public class CreateClientFavoritePropertyCommandHandler : IRequestHandler<CreateClientFavoritePropertyCommand, Response<int>>
    {
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;
        private readonly IMapper _mapper;

        public CreateClientFavoritePropertyCommandHandler(IClientFavoritePropertyRepository clientFavoritePropertyRepository, IMapper mapper)
        {
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateClientFavoritePropertyCommand command, CancellationToken cancellationToken)
        {
            var clientFavoriteProperty = _mapper.Map<RealStateApp.Core.Domain.Entities.ClientFavoriteProperty>(command);
            await _clientFavoritePropertyRepository.AddAsync(clientFavoriteProperty);
            return new Response<int>(clientFavoriteProperty.Id);
        }
    }
}
