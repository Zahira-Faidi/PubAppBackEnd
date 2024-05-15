using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.CreateProductCommand
{
    public record CreateProductCommand(
        string Name,
        //string Description,
        string Image,
        double Price,
        int Quantity,
        string CategoryId
        //List<string> Promotions
        ) : IRequest<ProductEntity>;

}
