using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.ClientFavoriteProperty.Commands.DeleteClientFavoritePropertyById
{
    /// <summary>
    /// Parametos para la eliminacion de una propiedad como favorito
    /// </summary>
    public class DeleteClientFavoritePropertyByIdCommand : IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El id del registro que se va a eliminar como favorito")]
        public int Id { get; set; }
    }

    public class DeleteClientFavoritePropertyByIdCommandHandler : IRequestHandler<DeleteClientFavoritePropertyByIdCommand, Response<int>>
    {
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;

        public DeleteClientFavoritePropertyByIdCommandHandler(IClientFavoritePropertyRepository clientFavoritePropertyRepository)
        {
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
        }

        public async Task<Response<int>> Handle(DeleteClientFavoritePropertyByIdCommand command, CancellationToken cancellationToken)
        {
            var clientFavoriteProperty = await _clientFavoritePropertyRepository.GetEntityByIdAsync(command.Id);

            if (clientFavoriteProperty == null) throw new ApiException("Products not found", (int)HttpStatusCode.NotFound);

            await _clientFavoritePropertyRepository.DeleteAsync(clientFavoriteProperty);

            return new Response<int>(clientFavoriteProperty.Id);
        }
    }
}
