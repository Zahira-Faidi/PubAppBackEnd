using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Commands.UpdateCreditCommand;

public class UpdateCreditCommandHandler : IRequestHandler<UpdateCreditCommand, CreditEntity>
{
    private readonly ICreditRepository _repository;
    public UpdateCreditCommandHandler(ICreditRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreditEntity> Handle(UpdateCreditCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingCredit = await _repository.GetDetailsAsync(request.Id, cancellationToken);

            if (existingCredit == null)
            {
                throw new Exception($"Credit with id {request.Id} not found");
            }

            existingCredit.Name = request.Name ?? existingCredit.Name;
            existingCredit.AvailableCredit = request.AvailableCredit !=0 ? request.AvailableCredit : existingCredit.AvailableCredit;
            existingCredit.Consumed = request.Consumed !=0 ? request.Consumed : existingCredit.Consumed;

            await _repository.UpdateAsync(request.Id, existingCredit, cancellationToken);

            return existingCredit;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update Credit: {ex.Message}", ex);
        }
        throw new NotImplementedException();
    }
}
