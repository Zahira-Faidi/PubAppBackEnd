using Ads.Domain.Enums;
using MediatR;

namespace Ads.Application.Campaigns.Commands.ActivateCampaignCommand;

public class ActivateCampaignCommand : IRequest<bool>
{
    public string CampaignId { get; set; }
    public Status Status { get; set; }
    public ActivateCampaignCommand(string campaignId , Status status)
    {
        CampaignId = campaignId;
        Status = status;
    }
}
