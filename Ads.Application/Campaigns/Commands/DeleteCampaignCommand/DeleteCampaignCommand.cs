using Ads.Domain.Entities;
using MediatR;

namespace Ads.Application.Campaigns.Commands.DeleteCampaignCommand
{
    public class DeleteCampaignCommand : IRequest<CampaignEntity>
    {
        public string Id { get; set; }
        public DeleteCampaignCommand(string id) 
        { 
            Id = id;
        }
    }
}
