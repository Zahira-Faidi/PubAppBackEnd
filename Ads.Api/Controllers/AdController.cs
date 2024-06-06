using Ads.Api.Common.Utils;
using Ads.Application.Ads.Commands.CreateAd;
using Ads.Application.Ads.Commands.DeleteAd;
using Ads.Application.Ads.Commands.UpdateAd;
using Ads.Application.Ads.Queries.GetAdById;
using Ads.Application.Ads.Queries.GetAdsByCampaignId;
using Ads.Application.Ads.Queries.GetAds;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ads.Application.Common.Exceptions;

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

            try
            {
                var query = new GetAdByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }
            catch (AdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred "+ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch(BudgetExceededException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAdCommand command, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(command.Id))
            {
                return BadRequest("Invalid id format");
            }
            try
            {
                return Ok(await _mediator.Send(command, cancellationToken));

            }
            catch (AdNotFoundException ex)
            {
                return NotFound(ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred " + ex.Message);
            }
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
            catch (AdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred " + ex.Message);
            }
        }

        [HttpGet("campaign/{campaignId}")]
        public async Task<IActionResult> GetAllAdsByCampaignId(string campaignId, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(campaignId))
            {
                return BadRequest("Invalid campaign ID format");
            }
            try
            {
                var query = new GetAdsByCampaignIdQuery(campaignId);
                var result = await _mediator.Send(query, cancellationToken);

                if (result == null)
                {
                    return NotFound($"No campaigns found for campaign with ID {campaignId}");
                }

                return Ok(result);
            }
            catch(CampaignNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}