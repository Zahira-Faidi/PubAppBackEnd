using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Queries.GetCampaignByIdSellerQuery
{
    public class GetCampaignByIdSellerQueryHandler : IRequestHandler<GetCampaignByIdSellerQuery, List<CampaignEntity>>
    {
        private readonly ICampaignRepository _campaignRepository;

        public GetCampaignByIdSellerQueryHandler(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<CampaignEntity>> Handle(GetCampaignByIdSellerQuery request, CancellationToken cancellationToken)
        {
            return await _campaignRepository.GetCampaignsBySeller(request.SellerId);
        }
    }
}
