using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

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
            try
            {
                var existingBudget = await _repository.GetDetailsAsync(request.Id, cancellationToken);
                if (existingBudget == null)
                {
                    throw new Exception($"Budget with id {request.Id} not found");
                }

                if (existingBudget.TotalBudget == 0)
                    existingBudget.TotalBudget = existingBudget.TotalBudget;
                else
                    existingBudget.TotalBudget = request.TotalBudget;
                if (existingBudget.DailyBudget == 0)
                    existingBudget.DailyBudget = existingBudget.DailyBudget;
                else
                    existingBudget.DailyBudget = request.DailyBudget;

                //existingBudget.Campaigns = request.Campaigns; // Update campaigns list if needed

                await _repository.UpdateAsync(request.Id , existingBudget, cancellationToken);

                return existingBudget;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update budget: {ex.Message}", ex);
            }
        }
    }
}
