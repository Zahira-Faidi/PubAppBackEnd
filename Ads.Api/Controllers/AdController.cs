using Ads.Api.Common.Interfaces;
using Ads.Application.Ads.Commands.CreateAdCommand;
using Ads.Application.Ads.Commands.DeleteAdCommand;
using Ads.Application.Ads.Commands.UpdateAdCommand;
using Ads.Application.Ads.Queries.GetAdByIdQuery;
using Ads.Application.Ads.Queries.GetAdsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase, ICommonController<CreateAdCommand, UpdateAdCommand>
    {
        private readonly IMediator _mediator;
        public AdController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAdsQuery();
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

            var query = new GetAdByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAdCommand command, CancellationToken cancellationToken)
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

            var command = new DeleteAdCommand(id);
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        private bool IsValidId(string id)
        {
            // Replace with your actual validation logic for ad IDs
            return !string.IsNullOrEmpty(id) && id.Length == 24 && id.All(char.IsLetterOrDigit);
        }
    }
}