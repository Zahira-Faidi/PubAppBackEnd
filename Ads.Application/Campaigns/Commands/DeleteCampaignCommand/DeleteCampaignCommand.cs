using Ads.Domain.Enums;
using MediatR;

namespace Ads.Application.Campaigns.Commands.DeleteCampaignCommand
{
    public class DeleteCampaignCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public Status Status { get; set; }
        public DeleteCampaignCommand(string id) 
        { 
            Id = id;
        }
    }
}
