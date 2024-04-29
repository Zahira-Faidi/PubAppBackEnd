using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.UpdateBudgetCommand
{
    public record UpdateBudgetCommand
        (
            string Id,
            double TotalBudget,
            double DailyBudget,
            List<string> Campaigns
        ) : IRequest<BudgetEntity>;
}
