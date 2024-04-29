using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.UpdateAdCommand
{
    public record UpdateAdCommand
        (
            string Id,
            string Content,
            double AllocatedBudget,
            string CampaignId
        ) : IRequest<AdEntity>;
}
