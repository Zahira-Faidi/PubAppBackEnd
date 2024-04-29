using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Ads.Commands.UpdateAdCommand
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, AdEntity>
    {
        private readonly ICommonRepository<AdEntity> _repository;
        public UpdateAdCommandHandler(ICommonRepository<AdEntity> repository)
        {
            _repository = repository;
        }

        public async Task<AdEntity> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingAd = await _repository.GetDetailsAsync(request.Id, cancellationToken);
                if (existingAd == null)
                {
                    throw new Exception($"Ad with id {request.Id} not found");
                }

                existingAd.Content = request.Content;
                existingAd.AllocatedBudget = request.AllocatedBudget;
                existingAd.CampaignId = request.CampaignId; 

                await _repository.UpdateAsync(existingAd, cancellationToken);

                return existingAd;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Ad: {ex.Message}", ex);
            }
        }
    }
}
