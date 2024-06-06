using Ads.Application.Common.Exceptions;
using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ads.Application.Ads.Commands.CreateAd
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, AdEntity>
    {
        private readonly IAdRepository _adRepository;
        private readonly IBudgetRepository _budgetRepository;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAdCommandHandler> _logger;

        public CreateAdCommandHandler(
            IAdRepository adRepository,
            IMapper mapper,
            IBudgetRepository budgetRepository,
            ICampaignRepository campaignRepository,
            ILogger<CreateAdCommandHandler> logger)
        {
            _adRepository = adRepository;
            _mapper = mapper;
            _budgetRepository = budgetRepository;
            _campaignRepository = campaignRepository;
            _logger = logger;
        }

        public async Task<AdEntity> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateAdCommand for CampaignId: {CampaignId}", request.CampaignId);

            // Retrieve campaign details
            var campaign = await _campaignRepository.GetDetailsAsync(request.CampaignId, cancellationToken);
            if (campaign == null)
            {
                _logger.LogError("Campaign not found for CampaignId: {CampaignId}", request.CampaignId);
                throw new CampaignNotFoundException("Campaign not found");
            }

            // Retrieve budget details
            var budget = await _budgetRepository.GetDetailsAsync(campaign.BudgetId, cancellationToken);
            if (budget == null)
            {
                _logger.LogError("Budget not found for BudgetId: {BudgetId}", campaign.BudgetId);
                throw new BudgetNotFoundException("Budget not found");
            }

            // Calculate the total credit used by all ads in the campaign
            var ads = await _adRepository.GetAllAdsByCampaignId(request.CampaignId, cancellationToken);
            double totalCreditUsed = ads.Sum(ad => ad.Credit);

            // Check if the new ad's credit exceeds the total available budget
            if (totalCreditUsed + request.Credit > budget.TotalBudget)
            {
                _logger.LogError("Total ad credits exceed campaign budget for CampaignId: {CampaignId}", request.CampaignId);
                throw new BudgetExceededException("Total ad credits exceed campaign budget");
            }

            // Map request to AdEntity and insert new ad
            var newAd = _mapper.Map<AdEntity>(request);
            var result = await _adRepository.InsertAsync(newAd, cancellationToken);

            // Update the consumed budget
            campaign.Consumed += request.Credit;
            await _campaignRepository.UpdateAsync(request.CampaignId, campaign, cancellationToken);

            _logger.LogInformation("Ad created successfully for CampaignId: {CampaignId}", request.CampaignId);
            return result;
        }
    }
}
