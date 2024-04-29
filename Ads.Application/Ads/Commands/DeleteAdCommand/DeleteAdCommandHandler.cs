using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.DeleteAdCommand
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, AdEntity>
    {
        private readonly ICommonRepository<AdEntity> _repository;
        public DeleteAdCommandHandler(ICommonRepository<AdEntity> repository)
        {
            _repository = repository;
        }
        public async Task<AdEntity> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var adToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (adToDelete == null)
                throw new Exception($"Ad with ID {request.Id} not found.");
            else
                await _repository.DeleteAsync(request.Id, cancellationToken);
            return adToDelete;
        }
    }
}
