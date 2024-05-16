using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Domain.Enums;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class CampaignRepository : BaseRepository<CampaignEntity>, ICampaignRepository
    {
        private readonly IMongoDatabase _database;

        public CampaignRepository(IMongoDatabase database) : base(database, "Campaigns")
        {
            _database = database;
        }

        public async Task<bool> ActivateCampaign(string id, Status status, CancellationToken cancellationToken)
        {
            var campaign = await GetDetailsAsync(id, cancellationToken);
            if(campaign.Status == Status.Inactive)
            {
                campaign.Status = Status.Active;
            }
            var updatedCampaign = await UpdateAsync(id, campaign, cancellationToken);
            if(updatedCampaign.Status == Status.Active) 
                return true;
            else
                return false;
        }

        public async Task<bool> DesactiverCampaign(string id, Status status, CancellationToken cancellationToken)
        {
            var campaign = await GetDetailsAsync(id, cancellationToken);
            if (campaign.Status == Status.Active)
            {
                campaign.Status = Status.Inactive;
            }
            var updatedCampaign = await UpdateAsync(id, campaign, cancellationToken);
            if (updatedCampaign.Status == Status.Inactive)
                return true;
            else
                return false;
        }

        public async Task<List<CampaignEntity>> GetCampaignsBySeller(string sellerId)
        {
            var collection = _database.GetCollection<CampaignEntity>("Campaigns");

            var filter = Builders<CampaignEntity>.Filter.Eq(x => x.SellerId, sellerId);

            return await collection.Find(filter).ToListAsync();
        }
    }
}
