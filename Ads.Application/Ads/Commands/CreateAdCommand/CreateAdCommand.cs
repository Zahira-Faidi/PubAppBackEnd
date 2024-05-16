using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.CreateAdCommand
{
    public record CreateAdCommand
        (
            string Name,
            string CampaignId,
            string CreditId
        ) : IRequest<AdEntity>;
}
