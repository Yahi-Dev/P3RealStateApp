using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Features.Improvement.Commands.CreateImprovement;
using RealStateApp.Core.Application.Features.Improvement.Commands.DeleteImprovement;
using RealStateApp.Core.Application.Features.Improvement.Commands.UpdateImprovement;
using RealStateApp.Core.Application.Features.Improvement.Queries.GetAllImprovement;
using RealStateApp.Core.Application.Features.Improvement.Queries.GetImprovementById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.WebApi.Controllers.v1
{
    [SwaggerTag("Improvement Functionalities")]

    public class ImprovementController : BaseApiController
    {
        [SwaggerOperation(
            Summary = "Create Improvement",
            Description = "Creates a new improvement."
        )]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CreateImprovementCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }

        [SwaggerOperation(
            Summary = "Update Improvement",
            Description = "Updates an existing improvement by its ID."
        )]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateImprovementResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateImprovementCommand command, int id)
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
            Summary = "Get All Improvements",
            Description = "Retrieves all improvements."
        )]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BaseImprovementDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllImprovementQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseImprovementDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetImprovementByIdQuery(id)));
        }

        [SwaggerOperation(
            Summary = "Get Improvement by ID",
            Description = "Retrieves an improvement by its ID."
        )]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteImprovementByIdCommand(id));
            return NoContent();
        }
    }
}
