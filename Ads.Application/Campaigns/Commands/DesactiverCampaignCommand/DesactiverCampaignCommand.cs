using Ads.Domain.Enums;
using MediatR;

namespace Ads.Application.Campaigns.Commands.DesactiverCampaignCommand;

public class DesactiverCampaignCommand : IRequest<bool>
{
    public string CampaignId { get; set; }
    public Status Status { get; set; }
    public DesactiverCampaignCommand(string campaignId, Status status)
    {
        CampaignId = campaignId;
        Status = status;
    }
}