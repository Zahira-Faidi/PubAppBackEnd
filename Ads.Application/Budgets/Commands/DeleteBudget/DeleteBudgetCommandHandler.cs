using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using MediatR;

namespace Ads.Application.Budgets.Commands.DeleteBudget
{
    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, Unit>
    {
        private readonly IBudgetRepository _repository;
        public DeleteBudgetCommandHandler(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            var budgetToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (budgetToDelete == null)
                throw new BudgetNotFoundException($"Budget with ID {request.Id} not found.");
            else
                await _repository.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
