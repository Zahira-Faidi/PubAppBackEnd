using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Commands.DeletePromotionCommand
{
    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand, PromotionEntity>
    {
        private readonly ICommonRepository<PromotionEntity> _repository;

        public DeletePromotionCommandHandler(ICommonRepository<PromotionEntity> promotionRepository)
        {
            _repository = promotionRepository;
        }

        public async Task<PromotionEntity> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotionToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (promotionToDelete == null)
            {
                throw new Exception($"Promotion with ID {request.Id} not found.");
            }
            else
            {
                await _repository.DeleteAsync(request.Id, cancellationToken);
            }
            return promotionToDelete;
        }
    }
}
