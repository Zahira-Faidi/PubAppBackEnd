using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Queries.GetBudgetByIdQuery
{
    public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetEntity>
    {
        private readonly IBudgetRepository _repository;
        public GetBudgetByIdQueryHandler(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public async Task<BudgetEntity> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetDetailsAsync(request.Id, cancellationToken);
        }
    }
}
