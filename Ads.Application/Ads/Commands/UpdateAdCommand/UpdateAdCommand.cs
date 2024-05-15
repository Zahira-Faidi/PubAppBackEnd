using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.UpdateAdCommand
{
    public record UpdateAdCommand
        (
            string Id,
            string Name,
            DateTime StartDate,
            DateTime EndDate,
            string CampaignId
        ) : IRequest<AdEntity>;
}
