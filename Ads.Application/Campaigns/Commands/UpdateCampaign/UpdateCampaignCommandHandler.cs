using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Commands.UpdateCampaignCommand
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, CampaignEntity>
    {
        private readonly ICampaignRepository _repository;
        public UpdateCampaignCommandHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<CampaignEntity> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCampaign = await _repository.GetDetailsAsync(request.Id, cancellationToken);
                if (existingCampaign == null)
                {
                    throw new Exception($"Campaign with id {request.Id} not found");
                }
                existingCampaign.Name = request.Name ?? existingCampaign.Name;
                existingCampaign.StartDate = request.StartDate ;
                existingCampaign.EndDate = request.EndDate;
                existingCampaign.BudgetId = request.BudgetId ?? existingCampaign.BudgetId;
                existingCampaign.Impressions = request.Impressions != 0 ? request.Impressions : existingCampaign.Impressions;
                existingCampaign.Consumed = request.Consumed != 0 ? request.Consumed : existingCampaign.Consumed;
                await _repository.UpdateAsync(request.Id, existingCampaign, cancellationToken);

                return existingCampaign;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update campaign: {ex.Message}", ex);
            }
        }
    }
}
