using Ads.Application.Common.Interfaces;
using MediatR;

namespace Ads.Application.Campaigns.Commands.ActivateCampaignCommand;

public class ActivateCampaignCommandHandler : IRequestHandler<ActivateCampaignCommand, bool>
{
    private readonly ICampaignRepository _repository;
    public ActivateCampaignCommandHandler(ICampaignRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(ActivateCampaignCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.ActivateCampaign(request.CampaignId , request.Status, cancellationToken);
        return result;
    }
}
