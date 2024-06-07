using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ads.Application.Budgets.Commands.UpdateBudgetCommand
{
    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, BudgetEntity>
    {
        private readonly IBudgetRepository _repository;

        public UpdateBudgetCommandHandler(IBudgetRepository repository)
        {
            _repository = repository;
        }

        public async Task<BudgetEntity> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {
            var existingBudget = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (existingBudget == null)
            {
                throw new BudgetNotFoundException($"Budget with id {request.Id} not found");
            }

            existingBudget.Name = request.Name ?? existingBudget.Name;
            existingBudget.TotalBudget = request.TotalBudget;

            await _repository.UpdateAsync(request.Id, existingBudget, cancellationToken);

            return existingBudget;
        }
    }
}
