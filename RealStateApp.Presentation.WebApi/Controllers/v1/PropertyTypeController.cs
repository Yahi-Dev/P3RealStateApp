using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Commands.DeletePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Queries.GetAllPropertyTypes;
using RealStateApp.Core.Application.Features.PropertyType.Queries.GetPropertyTypeById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Property Type Functionalities")]
    public class PropertyTypeController : BaseApiController
    {
        [SwaggerOperation(
            Summary = "Create Property Type",
            Description = "Creates a new property type."
        )]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Post([FromBody] CreatePropertyTypeCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }

        [SwaggerOperation(
            Summary = "Update Property Type",
            Description = "Updates an existing property type by its ID."
        )]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatePropertyTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdatePropertyTypeCommand command, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await Mediator.Send(command));
        }


        [SwaggerOperation(
            Summary = "Get All Property Types",
            Description = "Retrieves all property types."
        )]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BasePropertyTypeDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllPropertyTypesQuery()));
        }

        [SwaggerOperation(
            Summary = "Get Property Type by ID",
            Description = "Retrieves a property type by its ID."
        )]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePropertyTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> Get(int id)
        {
             return Ok(await Mediator.Send(new GetPropertyTypeByIdQuery(id))); ;
        }

        [SwaggerOperation(
            Summary = "Delete Property Type by ID",
            Description = "Deletes a property type by its ID."
        )]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePropertyTypeByIdCommand(id));
            return NoContent();
        }
    }
}
