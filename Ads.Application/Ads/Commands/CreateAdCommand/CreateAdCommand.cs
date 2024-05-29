using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.CreateAdCommand
{
    public record CreateAdCommand
        (
            string Name,
            DateTimeOffset StartDate,
            DateTimeOffset EndDate,
            string CampaignId,
            double Credit
        ) : IRequest<AdEntity>;
}
