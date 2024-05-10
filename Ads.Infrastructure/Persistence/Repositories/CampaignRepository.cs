using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class CampaignRepository : BaseRepository<CampaignEntity> , ICampaignRepository
    {
        public CampaignRepository(IMongoDatabase database) : base(database, "Campaigns")
        {
        }
    }
}