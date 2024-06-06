using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Queries.GetCampaignByIdQuery
{
    public class GetCampaignByIdQuery : IRequest<CampaignEntity>
    {
        public string Id { get; set; }
        public GetCampaignByIdQuery(string id)
        {
            Id = id;
        }
    }
}
