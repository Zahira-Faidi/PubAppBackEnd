using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Commands.UpdateCampaignCommand
{
    public record UpdateCampaignCommand
    (
        string Id,
        string Name,
        DateTimeOffset StartDate,
        DateTimeOffset EndDate,
        int Impressions,
        string BudgetId
    ) : IRequest<CampaignEntity>;
}
