using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.UpdateAd
{
    public record UpdateAdCommand
        (
            string Id,
            string Name,
            DateTimeOffset StartDate,
            DateTimeOffset EndDate,
            string CampaignId,
            double Credit,
            double Consumed,
            int Impressions
        ) : IRequest<AdEntity>;
}
