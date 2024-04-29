using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.CreateBudgetCommand
{
    public record CreateBudgetCommand
        (
            double TotalBudget,
            double DailyBudget,
            List<string> Campaigns
        ) : IRequest<BudgetEntity>;
}
