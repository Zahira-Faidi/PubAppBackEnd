using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Queries.GetBudgets
{
    public record GetBudgetsQuery : IRequest<List<BudgetEntity>>;
}
