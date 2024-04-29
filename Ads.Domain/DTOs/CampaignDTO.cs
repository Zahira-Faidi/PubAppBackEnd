using Ads.Domain.Enums;

namespace Ads.Domain.DTOs
{
    public record CampaignGetDto
    (
        string Id,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        double Budget,
        Status Status,
        List<string> Ads,
        string BudgetId
    );

    public record CampaignCreateDto
    (
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        double Budget,
        Status Status,
        List<string> Ads,
        string BudgetId
    );

    public record CampaignUpdateDto
    (
        string Id,
        string Name,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        double Budget,
        Status Status,
        List<string> Ads,
        string BudgetId
    );
}
