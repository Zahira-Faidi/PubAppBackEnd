using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Queries.GetBudgetsQuery
{
    public record GetBudgetsQuery : IRequest<List<BudgetEntity>>;
}
