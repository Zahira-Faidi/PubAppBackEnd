using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.UpdateProductCommand
{
    public record UpdateProductCommand (
        string Id,
        string? Name,
        //string? Description,
        string Image,
        double Price,
        int Quantity,
        int CPC,
        string CategoryId
        //string PromotionId
        ) : IRequest<ProductEntity>;
}
