using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Ads.Application.Ads.Queries.GetAdsByCampaignId
{
    public class GetAdsByCampaignIdQueryHandler : IRequestHandler<GetAdsByCampaignIdQuery, List<AdEntity>>
    {
        private readonly IAdRepository _adRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly ILogger<GetAdsByCampaignIdQueryHandler> _logger;
        private readonly double _costPerClick;

        public GetAdsByCampaignIdQueryHandler(
            IAdRepository adRepository,
            IProductRepository productRepository,
            ILogger<GetAdsByCampaignIdQueryHandler> logger,
            IConfiguration configuration,
            ICampaignRepository campaignRepository
            )
        {
            _adRepository = adRepository;
            _productRepository = productRepository;
            _logger = logger;
            _costPerClick = configuration.GetValue<double>("CostPerClick", 0.5); // Load CPC from configuration
            _campaignRepository = campaignRepository;
        }

        public async Task<List<AdEntity>> Handle(GetAdsByCampaignIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch all ads for the given campaign
                var oldList = await _adRepository.GetAllAdsByCampaignId(request.CampaignId, cancellationToken);

                // Filter out deleted ads
                var ads = oldList.Where(ad => !ad.IsDeleted).ToList();

                // Initialize campaign clicks and consumed credit
                var totalClicks = 0;
                var totalConsumedCredit = 0.0;

                var updateTasks = new List<Task>();

                foreach (var ad in ads)
                {
                    var products = await _productRepository.GetAllProductsByAdId(ad.Id, cancellationToken);
                    var adClicks = products.Sum(product => product.Click);

                    // Update ad impressions and consumed credit
                    ad.Impressions = adClicks;
                    ad.Consumed = adClicks * _costPerClick;

                    totalClicks += adClicks;
                    totalConsumedCredit += ad.Consumed;

                    // Schedule the ad update task
                    updateTasks.Add(_adRepository.UpdateAsync(ad.Id, ad, cancellationToken));
                }

                // Wait for all ad update tasks to complete
            
                await Task.WhenAll(updateTasks);
                // Get Campagne
                var campaign = await _campaignRepository.GetDetailsAsync(request.CampaignId, cancellationToken);
                campaign.Impressions = totalClicks;
                await _campaignRepository.UpdateAsync(request.CampaignId , campaign , cancellationToken);
                return ads;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching ads for campaign ID {request.CampaignId}.");
                throw; // Re-throw the exception to ensure the caller is aware of the failure
            }
        }
    }
}
