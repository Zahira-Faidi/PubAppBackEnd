using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Queries.GetCampaignsQuery
{
    public record GetCampaignsQuery : IRequest<List<CampaignEntity>>;

}
