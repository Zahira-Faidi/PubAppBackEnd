using Ads.Application.Common.Interfaces;
using MediatR;

namespace Ads.Application.Campaigns.Commands.DesactiverCampaignCommand;

public class DesactiverCampaignCommandHandler : IRequestHandler<DesactiverCampaignCommand, bool>
{
    private readonly ICampaignRepository _repository;
    public DesactiverCampaignCommandHandler(ICampaignRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(DesactiverCampaignCommand request, CancellationToken cancellationToken)
    {
        var result = await _repository.DesactiverCampaign(request.CampaignId , request.Status , cancellationToken);
        return result;
    }
}
