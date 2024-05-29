using Ads.Application.Common.Interfaces;
using Ads.Domain.Enums;
using MediatR;

namespace Ads.Application.Campaigns.Commands.DeleteCampaignCommand
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, Unit>
    {
        private readonly ICampaignRepository _repository;
        public DeleteCampaignCommandHandler(ICampaignRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaignToDelete = await _repository.GetDetailsAsync(request.Id, cancellationToken);
            if (campaignToDelete == null)
                throw new Exception($"Campaign with ID {request.Id} not found.");
            else if(campaignToDelete.Status == Status.Active || campaignToDelete.Status == Status.Inactive)
            {
                throw new Exception($"You can't delete an Campaing with Active or Inactive Status");
            }
            else
                await _repository.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;        
        }
    }
}
