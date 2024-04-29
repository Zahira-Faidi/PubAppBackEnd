using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.CreateAdCommand
{
    public record CreateAdCommand
        (
            string Content,
            double AllocatedBudget,
            string CampaignId
        ) : IRequest<AdEntity>;
}
