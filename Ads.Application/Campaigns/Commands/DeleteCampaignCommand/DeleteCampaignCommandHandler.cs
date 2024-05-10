using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Commands.DeleteCampaignCommand
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, CampaignEntity>
    {
        private readonly ICampaignRepository _repository;
        public DeleteCampaignCommandHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<CampaignEntity> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaignToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (campaignToDelete == null)
                throw new Exception($"Campaign with ID {request.Id} not found.");
            else
                await _repository.DeleteAsync(request.Id, cancellationToken);
            return campaignToDelete;        
        }
    }
}
