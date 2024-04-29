﻿using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Commands.UpdateCampaignCommand
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, CampaignEntity>
    {
        private readonly ICommonRepository<CampaignEntity> _repository;
        public UpdateCampaignCommandHandler(ICommonRepository<CampaignEntity> repository)
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
                existingCampaign.Description = request.Description ?? existingCampaign.Description;
                existingCampaign.StartDate = request.StartDate;
                existingCampaign.EndDate = request.EndDate;
                existingCampaign.Budget = request.Budget;
                existingCampaign.Status = request.Status;
                existingCampaign.BudgetId = request.BudgetId ?? existingCampaign.BudgetId;
                existingCampaign.Ads = request.Ads;
                await _repository.UpdateAsync(existingCampaign, cancellationToken);

                return existingCampaign;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update campaign: {ex.Message}", ex);
            }
        }
    }
}
