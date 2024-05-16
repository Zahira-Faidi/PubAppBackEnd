using Ads.Domain.Entities;
using Ads.Domain.Enums;
using MediatR;

namespace Ads.Application.Campaigns.Commands.CreateCampaignCommand
{
    public record CreateCampaignCommand
    (
        string Name,
        //string Description,
        DateTimeOffset StartDate,
        DateTimeOffset EndDate,
        //double Budget,
        int Impressions,
        string SellerId,
        Status Status,
        //List<string>? Ads,
        string? BudgetId
    ) :IRequest<CampaignEntity>;
}
