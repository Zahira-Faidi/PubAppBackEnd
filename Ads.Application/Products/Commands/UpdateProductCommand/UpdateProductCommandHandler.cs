using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Products.Commands.UpdateProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductEntity>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductEntity> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await _repository.GetDetailsAsync(request.Id, cancellationToken);

                if (existingProduct == null)
                {
                    throw new Exception($"Product with id {request.Id} not found");
                }

                // Update existingProduct properties based on request
                existingProduct.Name = request.Name ?? existingProduct.Name;
                existingProduct.Image = request.Image ?? existingProduct.Image;
                existingProduct.Price = request.Price != 0 ? request.Price : existingProduct.Price;
                existingProduct.Quantity = request.Quantity != 0 ? request.Quantity : existingProduct.Quantity;
                //existingProduct.CPC = request.CPC != 0 ? request.CPC : existingProduct.CPC;
                existingProduct.CategoryId = request.CategoryId ?? existingProduct.CategoryId;
                existingProduct.AdId = request.AdId ?? existingProduct.AdId;
                await _repository.UpdateAsync(request.Id, existingProduct, cancellationToken);

                return existingProduct;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update product: {ex.Message}", ex);
            }
        }
    }
}
