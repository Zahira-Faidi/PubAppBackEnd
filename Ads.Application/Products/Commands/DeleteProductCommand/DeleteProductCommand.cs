using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<ProductEntity>
    {
        public string ProductId { get; set; }
        public DeleteProductCommand(string productId)
        {
            ProductId = productId;
        }
    }
}
