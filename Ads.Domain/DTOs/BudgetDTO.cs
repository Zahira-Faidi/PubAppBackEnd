namespace Ads.Domain.DTOs
{
        public record BudgetGetDto
        (
            string Id,
            double TotalBudget,
            double DailyBudget,
            List<string> Campaigns
        );

        public record BudgetCreateDto
        (
            double TotalBudget,
            double DailyBudget,
            List<string> Campaigns
        );

        public record BudgetUpdateDto
        (
            string Id,
            double TotalBudget,
            double DailyBudget,
            List<string> Campaigns
        );
}
