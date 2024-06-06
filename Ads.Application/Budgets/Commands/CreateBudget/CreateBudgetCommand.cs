using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.CreateBudget
{
    public record CreateBudgetCommand
        (
            string Name,
            double TotalBudget
        ) : IRequest<BudgetEntity>;
}
