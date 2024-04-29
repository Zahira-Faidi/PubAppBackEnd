using Ads.Api.Common.Interfaces;
using Ads.Application.Categories.Commands.CreateCategoryCommand;
using Ads.Application.Categories.Commands.DeleteCategoryCommand;
using Ads.Application.Categories.Commands.UpdateCategoryCommand;
using Ads.Application.Categories.Queries.GetCategoriesQuery;
using Ads.Application.Categories.Queries.GetCategoryByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase, ICommonController<CreateCategoryCommand, UpdateCategoryCommand>
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetCategoriesQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            if (!IsValidId(id))
            {
                return BadRequest("Invalid id format");
            }

            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            if (!IsValidId(command.Id))
            {
                return BadRequest("Invalid id format or id mismatch");
            }
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            if (!IsValidId(id))
            {
                return BadRequest("Invalid id format");
            }

            var command = new DeleteCategoryCommand(id);
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        private bool IsValidId(string id)
        {
            return !string.IsNullOrEmpty(id) && id.Length == 24 && id.All(char.IsLetterOrDigit);
        }
    }
}
