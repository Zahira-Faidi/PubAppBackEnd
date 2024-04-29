using Ads.Api.Common.Interfaces;
using Ads.Application.Promotions.Commands.CreatePromotionCommand;
using Ads.Application.Promotions.Commands.DeletePromotionCommand;
using Ads.Application.Promotions.Commands.UpdatePromotionCommand;
using Ads.Application.Promotions.Queries.GetPromotionByIdQuery;
using Ads.Application.Promotions.Queries.GetPromotionsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase, ICommonController<CreatePromotionCommand, UpdatePromotionCommand>
    {
        private readonly IMediator _mediator;

        public PromotionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetPromotionsQuery();
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

            var query = new GetPromotionByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromotionCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePromotionCommand command, CancellationToken cancellationToken)
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

            var command = new DeletePromotionCommand(id);
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        private bool IsValidId(string id)
        {
            return !string.IsNullOrEmpty(id) && id.Length == 24 && id.All(char.IsLetterOrDigit);
        }
    }
}
