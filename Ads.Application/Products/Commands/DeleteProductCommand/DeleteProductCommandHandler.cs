using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand , ProductEntity>
    {
        private readonly ICommonRepository<ProductEntity> _repository;
        public DeleteProductCommandHandler(ICommonRepository<ProductEntity> productRepository)
        {
            _repository = productRepository;
        }
        public async Task<ProductEntity> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _repository.GetDetailsAsync(request.ProductId, cancellationToken);
            if (productToDelete == null)
                throw new Exception($"Product with ID {request.ProductId} not found.");
            else
                await _repository.DeleteAsync(request.ProductId, cancellationToken);
            return productToDelete;
        }


    }
}
