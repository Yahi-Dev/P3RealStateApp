using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Features.SaleCategory.Commands.CreateSaleCategory;
using RealStateApp.Core.Application.Features.SaleCategory.Commands.DeleteSaleCategoryById;
using RealStateApp.Core.Application.Features.SaleCategory.Commands.UpdateSaleCategory;
using RealStateApp.Core.Application.Features.SaleCategory.Queries.GetAllSaleCategories;
using RealStateApp.Core.Application.Features.SaleCategory.Queries.GetSaleCategoryById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Sale Category Functionalities")]
    public class SaleCategoryController : BaseApiController
    {
        [SwaggerOperation(
            Summary = "Create Sale Category",
            Description = "Creates a new sale category for use with properties."
        )]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CreateSaleCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }

        [SwaggerOperation(
            Summary = "Update Sale Category",
            Description = "Updates a sale category for use with properties."
        )]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateSaleCategoryResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateSaleCategoryCommand command, int id)
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
            Summary = "Get All Sale Categories",
            Description = "Retrieves all sale categories available for use with properties."
        )]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<BaseSaleCategoryDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllSaleCategoriesQuery()));
        }


        [SwaggerOperation(
            Summary = "Get Sale Category by ID",
            Description = "Retrieves a sale category by its ID."
        )]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseSaleCategoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin, Developer")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetSaleCategoryByIdQuery(id)));
        }

        [SwaggerOperation(
            Summary = "Delete Sale Category by ID",
            Description = "Deletes a sale category by its ID."
        )]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSaleCategoryByIdCommand(id));
            return NoContent();
        }
    }
}
