using Ads.Application.Common.Interfaces;
using MediatR;

namespace Ads.Application.Ads.Commands.DeleteAdCommand
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, Unit>
    {
        private readonly IAdRepository _repository;

        public DeleteAdCommandHandler(IAdRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var adToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);

            if (adToDelete == null)
            {
                throw new Exception($"Ad with ID {request.Id} not found.");
            }

            adToDelete.IsDeleted = true;
            //await _repository.DeleteAsync(request.Id, cancellationToken);

            await _repository.UpdateAsync(request.Id, adToDelete, cancellationToken);

            return Unit.Value;
        }
    }
}
