using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Features.Property.Queries.GetAllProperties;
using RealStateApp.Core.Application.Features.Property.Queries.GetPropertyByCode;
using RealStateApp.Core.Application.Features.Property.Queries.GetPropertyById;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [SwaggerTag("Property Functionalities")]
    [Authorize(Roles = "Admin, Developer")]
    public class PropertyController : BaseApiController
    {





        [SwaggerOperation(
            Summary = "Get All Properties",
            Description = "Retrieves all properties.")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BasePropertyDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllPropertiesQuery()));
        }







        [SwaggerOperation(
            Summary = "Get Property by ID",
            Description = "Retrieves a property by its ID.")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePropertyDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPropertyByIdQuery(id)));
        }







        [SwaggerOperation(
            Summary = "Get Property by Code",
            Description = "Retrieves a property by its code.")]
        [HttpGet("code/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePropertyDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(string code)
        {
            return Ok(await Mediator.Send(new GetPropertyByCodeQuery(code)));
        }
    }
}
