using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.UpdateBudgetCommand
{
    public record UpdateBudgetCommand
        (
            string Id,
            string Name,
            double TotalBudget
        ) : IRequest<BudgetEntity>;
}
