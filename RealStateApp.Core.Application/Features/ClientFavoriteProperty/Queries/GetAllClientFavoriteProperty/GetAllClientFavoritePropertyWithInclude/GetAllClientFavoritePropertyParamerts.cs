using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.ClientFavoriteProperty.Queries.GetAllClientFavoriteProperty.GetAllClientFavoritePropertyWithInclude
{
    /// <summary>
    /// Parametros Para filtrar los clientes por propiedades
    /// </summary>
    public class GetAllClientFavoritePropertyParamerts
    {
        [SwaggerParameter(Description = "El Id de de la propiedad por la cual se va a filtrar")]
        public int? PropertyId { get; set; }

    }
}
