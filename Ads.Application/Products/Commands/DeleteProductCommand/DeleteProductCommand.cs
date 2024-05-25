using MediatR;

namespace Ads.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public string ProductId { get; set; }
        public DeleteProductCommand(string productId)
        {
            ProductId = productId;
        }
    }
}
