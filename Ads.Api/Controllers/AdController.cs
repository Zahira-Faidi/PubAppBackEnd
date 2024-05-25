using Ads.Api.Common.Utils;
using Ads.Application.Ads.Commands.CreateAdCommand;
using Ads.Application.Ads.Commands.DeleteAdCommand;
using Ads.Application.Ads.Commands.UpdateAdCommand;
using Ads.Application.Ads.Queries.GetAdByIdQuery;
using Ads.Application.Ads.Queries.GetAdsByCampaignIdQuery;
using Ads.Application.Ads.Queries.GetAdsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
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
            if (!ValidationUtils.IsValidId(id))
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

            try
            {
                var command = new DeleteAdCommand(id);
                await _mediator.Send(command, cancellationToken);
                return Ok("Ad deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET ALL ADS BY CAMPAIGNID
        [HttpGet("campaign/{campaignId}")]
        public async Task<IActionResult> GetAllAdsByCampaignId(string campaignId, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(campaignId))
            {
                return BadRequest("Invalid campaign ID format");
            }

            var query = new GetAdsByCampaignIdQuery(campaignId);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound($"No campaigns found for campaign with ID {campaignId}");
            }

            return Ok(result);
        }
    }
}