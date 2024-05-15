using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.CreateAdCommand
{
    public record CreateAdCommand
        (
            string Name,
            DateTime StartDate,
            DateTime EndDate,
            string CampaignId
        ) : IRequest<AdEntity>;
}
