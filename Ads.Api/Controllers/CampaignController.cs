using Ads.Api.Common.Utils;
using Ads.Application.Campaigns.Commands.ActivateCampaign;
using Ads.Application.Campaigns.Commands.CreateCampaign;
using Ads.Application.Campaigns.Commands.DeleteCampaign;
using Ads.Application.Campaigns.Commands.DesactiverCampaignCommand;
using Ads.Application.Campaigns.Commands.UpdateCampaignCommand;
using Ads.Application.Campaigns.Queries.GetCampaignByIdQuery;
using Ads.Application.Campaigns.Queries.GetCampaignByIdSellerQuery;
using Ads.Application.Campaigns.Queries.GetCampaignsQuery;
using Ads.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ads.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // START ACTIVATE CAMAPAIGN 
        [HttpPost("activate/{campaignId}")]
        public async Task<IActionResult> ActivateCampaign(string campaignId , CancellationToken cancellationToken)
        {
            try
            {
                var campaign = new GetCampaignByIdQuery(campaignId);
                var result = await _mediator.Send(campaign, cancellationToken);
                var command = new ActivateCampaignCommand(campaignId, result.Status);

                var res = await _mediator.Send(command);
                if (res)
                {
                    return Ok("Campaign activated successfully.");
                }
                else
                {
                    return BadRequest("Failed to activate campaign.");
                }
            }
            catch (CampaignNotFoundException ex)
            {
                return NotFound(ex.Message);  
            }

        }
        // START DESACTIVATE CAMAPAIGN 
       
        [HttpPost("desactivate/{campaignId}")]
        public async Task<IActionResult> DesactivateCampaign(string campaignId, CancellationToken cancellationToken)
        {
            try
            {
                var campaign = new GetCampaignByIdQuery(campaignId);
                var result = await _mediator.Send(campaign, cancellationToken);
                var command = new DesactiverCampaignCommand(campaignId, result.Status);
                var res = await _mediator.Send(command);
                if (res)
                {
                    return Ok("Campaign desactivated successfully.");
                }
                else
                {
                    return BadRequest("Failed to desactivated campaign.");
                }
            }
            catch (CampaignNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
        // START GET CAMPAIGN BY ID SELLER
        [HttpGet("seller/{sellerId}")]
        public async Task<IActionResult> GetBySellerId(string sellerId, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(sellerId))
            {
                return BadRequest("Invalid seller ID format");
            }

            var query = new GetCampaignByIdSellerQuery(sellerId);
            var result = await _mediator.Send(query, cancellationToken);

            if (result == null)
            {
                return NotFound($"No campaigns found for seller with ID {sellerId}");
            }

            return Ok(result);
        }
        // START GET ALL CAMPAIGNS
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetCampaignsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        // START GET CAMPAIGN BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(id))
            {
                return BadRequest("Invalid id format");
            }

            var query = new GetCampaignByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        // START CREATE CAMPAIGN
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCampaignCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        // START UPDATE CAMPAIGN
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCampaignCommand command, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(command.Id))
            {
                return BadRequest("Invalid id format");
            }

            return Ok(await _mediator.Send(command, cancellationToken));
        }
        // START DELETE CAMPAIGN
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            if (!ValidationUtils.IsValidId(id))
            {
                return BadRequest("Invalid id format");
            }
            try
            {
                await _mediator.Send(new DeleteCampaignCommand(id), cancellationToken);
                return Ok("Campaign deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
