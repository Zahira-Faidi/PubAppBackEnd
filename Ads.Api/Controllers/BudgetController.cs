using Ads.Api.Common.Interfaces;
using Ads.Application.Budgets.Commands.CreateBudgetCommand;
using Ads.Application.Budgets.Commands.DeleteBudgetCommand;
using Ads.Application.Budgets.Commands.UpdateBudgetCommand;
using Ads.Application.Budgets.Queries.GetBudgetByIdQuery;
using Ads.Application.Budgets.Queries.GetBudgetsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase, ICommonController<CreateBudgetCommand, UpdateBudgetCommand>
    {
        private readonly IMediator _mediator;

        public BudgetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetBudgetsQuery();
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

            var query = new GetBudgetByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBudgetCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBudgetCommand command, CancellationToken cancellationToken)
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

            var command = new DeleteBudgetCommand(id);
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        private bool IsValidId(string id)
        {
            // Replace with your actual validation logic for budget IDs
            return !string.IsNullOrEmpty(id) && id.Length == 24 && id.All(char.IsLetterOrDigit);
        }
    }
}
