using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Queries.GetCampaignByIdSellerQuery
{
    public class GetCampaignByIdSellerQuery : IRequest<List<CampaignEntity>>
    {
        public string SellerId { get; set; }
        public GetCampaignByIdSellerQuery(string sellerId)
        {
            SellerId = sellerId;
        }
    }
}
