using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ads.Application.Products.Queries.GetAllProductsByAdIdQuery
{
    public class GetAllProductsByAdIdQueryHandler : IRequestHandler<GetAllProductsByAdIdQuery, List<ProductEntity>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IAdRepository _adRepository;
        private readonly ILogger<GetAllProductsByAdIdQueryHandler> _logger;

        public GetAllProductsByAdIdQueryHandler(IProductRepository productRepository, IAdRepository adRepository, ILogger<GetAllProductsByAdIdQueryHandler> logger)
        {
            _productRepository = productRepository;
            _adRepository = adRepository;
            _logger = logger;
        }

        public async Task<List<ProductEntity>> Handle(GetAllProductsByAdIdQuery request, CancellationToken cancellationToken)
        {
            List<ProductEntity> products = new List<ProductEntity>();

            try
            {
                // Fetch all products associated with the specified AdId
                products = await _productRepository.GetAllProductsByAdId(request.AdId, cancellationToken);

                if (products == null || !products.Any())
                {
                    _logger.LogWarning($"No products found for AdId: {request.AdId}");
                    return products;
                }

                // Calculate the total number of clicks (impressions) for the products
                var totalImpressions = products.Sum(product => product.Click);

                // Fetch the ad details
                var ad = await _adRepository.GetDetailsAsync(request.AdId, cancellationToken);
                if (ad == null)
                {
                    _logger.LogError($"Ad not found for AdId: {request.AdId}");
                    return products;
                }

                // Update the impressions count of the ad
                ad.Impressions = totalImpressions;
                await _adRepository.UpdateAsync(request.AdId, ad, cancellationToken);

                _logger.LogInformation($"Updated ad {request.AdId} with {totalImpressions} impressions.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to handle GetAllProductsByAdIdQuery for AdId: {request.AdId}");
                throw; // Re-throw the exception to ensure the caller is aware of the failure
            }

            return products;
        }
    }
}
