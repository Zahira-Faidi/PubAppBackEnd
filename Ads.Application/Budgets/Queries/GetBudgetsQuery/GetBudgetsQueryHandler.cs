using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Queries.GetBudgetsQuery
{
    public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, List<BudgetEntity>>
    {
        private readonly ICommonRepository<BudgetEntity> _repository;
        public GetBudgetsQueryHandler(ICommonRepository<BudgetEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<BudgetEntity>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }
    }
}
