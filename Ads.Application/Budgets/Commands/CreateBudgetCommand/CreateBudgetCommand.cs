using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.CreateBudgetCommand
{
    public record CreateBudgetCommand
        (
            double TotalBudget,
            double DailyBudget
        ) : IRequest<BudgetEntity>;
}
