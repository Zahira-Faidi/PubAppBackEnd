using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Promotions.Commands.UpdatePromotionCommand
{
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, PromotionEntity>
    {
        private readonly IPromotionRepository _repository;

        public UpdatePromotionCommandHandler(IPromotionRepository promotionRepository)
        {
            _repository = promotionRepository;
        }

        public async Task<PromotionEntity> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotionToUpdate = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (promotionToUpdate == null)
            {
                throw new Exception($"Promotion with ID {request.Id} not found.");
            }

            // Créer une nouvelle entité PromotionEntity avec les valeurs de la commande de mise à jour
            var updatedPromotion = new PromotionEntity
            {
                Id = request.Id,
                Description = request.Description ?? promotionToUpdate.Description,
                Discount = request.Discount,
                Products = request.Products ?? promotionToUpdate.Products
            };

            try
            {
                await _repository.UpdateAsync(request.Id,updatedPromotion, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update promotion: {ex.Message}", ex);
            }

            return updatedPromotion;
        }
    }
}
