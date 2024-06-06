using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Commands.CreateCampaign
{
    public record CreateCampaignCommand
    (
        string Name,
        DateTimeOffset StartDate,
        DateTimeOffset EndDate,
        int Impressions, // default 0
        string SellerId, // is getting from the local storage
        string BudgetId
    ) :IRequest<CampaignEntity>;
}
