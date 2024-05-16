using Ads.Application.Common.Base;
using Ads.Domain.Entities;
using Ads.Domain.Enums;

namespace Ads.Application.Common.Interfaces
{
    public interface ICampaignRepository : IBaseRepository<CampaignEntity>
    {
        Task<List<CampaignEntity>> GetCampaignsBySeller(string sellerId);
        Task<bool> ActivateCampaign(string id , Status status, CancellationToken cancellationToken);
        Task<bool> DesactiverCampaign(string id, Status status, CancellationToken cancellationToken);
    }
}
