using Ads.Domain.Entities;
using Ads.Domain.Enums;
using MediatR;

namespace Ads.Application.Campaigns.Commands.CreateCampaignCommand
{
    public record CreateCampaignCommand
    (
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        double Budget,
        Status Status,
        List<string>? Ads,
        string? BudgetId
    ) :IRequest<CampaignEntity>;
}
