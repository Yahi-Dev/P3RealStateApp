using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Features.Agent.Commands;
using RealStateApp.Core.Application.Features.Agent.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.WebApi.Controllers.v1
{
    [SwaggerTag("Agent Functionalities")]
    [Authorize(Roles = "Admin,Developer")]
    public class AgentController : BaseApiController
    {
        [SwaggerOperation(
            Summary = "Get All Agents",
            Description = "Retrieves all agents registered."
        )]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<DtoAccounts>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllAgentsQuery()));
        }


        [SwaggerOperation(
            Summary = "Get Agent Properties",
            Description = "Retrieves properties associated with a specific agent.")]
        [HttpGet("agent-properties")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BasePropertyDto>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgentProperties(string id)
        {
            return Ok(await Mediator.Send(new GetAgentPropertyQuery(id)));
        }




        [SwaggerOperation(
            Summary = "Get Agent by ID",
            Description = "Retrieves an agent by its ID."
        )]
        [HttpGet("Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoAccounts))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await Mediator.Send(new GetAgentByIdQuery(id)));
        }





        [SwaggerOperation(
            Summary = "Change Agent Status",
            Description = "Changes the status of an agent to active or inactive, this affects the things the agent can make in the app."
        )]
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Patch(string id, bool isActive)
        {
                await Mediator.Send(new ChangeAgentStatusCommand(id, isActive));
                return NoContent();
        }
    }
}
