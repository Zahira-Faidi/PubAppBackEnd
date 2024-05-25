using Ads.Api.Common.Utils;
using Ads.Application.Ads.Queries.GetAdsByCampaignIdQuery;
using Ads.Application.Campaigns.Queries.GetCampaignByIdQuery;
using Ads.Application.Products.Commands.CreateProductCommand;
using Ads.Application.Products.Commands.DeleteProductCommand;
using Ads.Application.Products.Commands.UpdateProductCommand;
using Ads.Application.Products.Queries.GetAllProductsByAdIdQuery;
using Ads.Application.Products.Queries.GetProductByIdQuery;
using Ads.Application.Products.Queries.GetProductsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Ads.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ApiController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery();
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

        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        if (result.IsError) return BadRequest(result.Errors);
        
        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
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
            var command = new DeleteProductCommand(id);
            await _mediator.Send(command, cancellationToken);
            return Ok("Product deleted successfully");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    // GET Product By Id AD
    [HttpGet("ad/{id}")]
    public async Task<IActionResult> GetAllProductByAdId(string id, CancellationToken cancellationToken)
    {
        if (!ValidationUtils.IsValidId(id))
        {
            return BadRequest("Invalid ad ID format");
        }

        var query = new GetAllProductsByAdIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        if (result == null)
        {
            return NotFound($"No ads found for ad with ID {id}");
        }

        return Ok(result);
    }
}