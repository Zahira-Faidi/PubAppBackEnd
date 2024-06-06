using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.CreateProductCommand
{
    public record CreateProductCommand(
        string Name,
        string Image, // This should be a string
        double Price,
        int Quantity,
        string CategoryId,
        string AdId
    ) : IRequest<ProductEntity>;
}
