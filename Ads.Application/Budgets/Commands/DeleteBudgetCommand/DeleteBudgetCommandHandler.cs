using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Budgets.Commands.DeleteBudgetCommand
{
    public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, BudgetEntity>
    {
        private readonly ICommonRepository<BudgetEntity> _repository;
        public DeleteBudgetCommandHandler(ICommonRepository<BudgetEntity> repository)
        {
            _repository = repository;
        }

        public async Task<BudgetEntity> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            var budgetToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (budgetToDelete == null)
                throw new Exception($"Budget with ID {request.Id} not found.");
            else
                await _repository.DeleteAsync(request.Id, cancellationToken);
            return budgetToDelete;
        }
    }
}
