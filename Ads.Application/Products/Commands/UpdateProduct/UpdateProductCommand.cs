using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.UpdateProductCommand
{
    public record UpdateProductCommand(
        string Id,
        string? Name,
        string? Image,
        double Price,
        int Quantity,
        int CPC,
        int Click,
        string? CategoryId,
        string? AdId
        ) : IRequest<ProductEntity>;
}
