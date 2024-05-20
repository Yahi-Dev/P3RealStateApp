using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealStateApp.Core.Application.Features.ClientFavoriteProperty.Queries.GetClientFavoritePropertyById
{
    /// <summary>
    /// Parametros para obtener un Client Favorite Property por Id
    /// </summary>
    public class GetClientFavoritePropertyByIdQuery : IRequest<Response<BaseClientFavoriteProperty>>
    {
        [SwaggerParameter(Description = "Debe colocar el Id del registro que quiere obtener")]
        public int Id { get; set; }
    }

    public class GetClientFavoritePropertyByIdQueryHandler : IRequestHandler<GetClientFavoritePropertyByIdQuery, Response<BaseClientFavoriteProperty>>
    {
        private readonly IClientFavoritePropertyRepository _clientFavoritePropertyRepository;
        private readonly IMapper _mapper;

        public GetClientFavoritePropertyByIdQueryHandler(IClientFavoritePropertyRepository clientFavoritePropertyRepository, IMapper mapper)
        {
            _clientFavoritePropertyRepository = clientFavoritePropertyRepository;
            _mapper = mapper;
        }
        public async Task<Response<BaseClientFavoriteProperty>> Handle(GetClientFavoritePropertyByIdQuery request, CancellationToken cancellationToken)
        {
            var clientFavoriteProperty = await GetByIdViewModel(request.Id);
            if (clientFavoriteProperty == null) throw new ApiException("Registro not found", (int)HttpStatusCode.NotFound);
            return new Response<BaseClientFavoriteProperty>(clientFavoriteProperty);
        }

        private async Task<BaseClientFavoriteProperty> GetByIdViewModel(int id)
        {
            var clientFavoritePropertyList = await _clientFavoritePropertyRepository.GetAllWithIncludeAsync(new List<string> { "Category" });

            var clientFavoriteProperty = clientFavoritePropertyList.FirstOrDefault(f => f.Id == id);

            BaseClientFavoriteProperty property = new()
            {
                Id = clientFavoriteProperty.Id,
                ClientId = clientFavoriteProperty.ClientId,
                PropertyId = clientFavoriteProperty.PropertyId
            };

            return property;
        }
    }
}
