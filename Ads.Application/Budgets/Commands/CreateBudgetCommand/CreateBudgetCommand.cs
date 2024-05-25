using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.CreateBudgetCommand
{
    public record CreateBudgetCommand
        (
            string Name,
            double TotalBudget
            //double DailyBudget
        ) : IRequest<BudgetEntity>;
}
