using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Credits.Commands.DeleteCreditCommand;

public class DeleteCreditCommandHandler : IRequestHandler<DeleteCreditCommand, Unit>
{
    private readonly ICreditRepository _repository;
    public DeleteCreditCommandHandler(ICreditRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCreditCommand request, CancellationToken cancellationToken)
    {
        var creditToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
        if (creditToDelete == null)
            throw new Exception($"Credit with ID {request.Id} not found.");
        else
            await _repository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
