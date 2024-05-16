using Ads.Api.Common.Utils;
using Ads.Application.Credits.Commands.CreateCreditCommand;
using Ads.Application.Credits.Commands.DeleteCreditCommand;
using Ads.Application.Credits.Commands.UpdateCreditCommand;
using Ads.Application.Credits.Queries.GetCreditByIdQuery;
using Ads.Application.Credits.Queries.GetCreditsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CreditController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetCreditsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(id))
            {
                return BadRequest("Invalid id format");
            }

            var query = new GetCreditByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCreditCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCreditCommand command, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(command.Id))
            {
                return BadRequest("Invalid id format");
            }
            return Ok(await _mediator.Send(command, cancellationToken));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(id))
            {
                return BadRequest("Invalid id format");
            }

            var command = new DeleteCreditCommand(id);
            return Ok(await _mediator.Send(command, cancellationToken));
        }
    }
}
