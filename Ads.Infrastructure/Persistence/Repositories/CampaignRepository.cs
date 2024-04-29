using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Persistence.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class CampaignRepository : ICommonRepository<CampaignEntity>
    {
        private readonly IMongoCollection<CampaignEntity> _campaignsCollection;

        public CampaignRepository(IMongoDatabase database, IOptions<DataBaseSettings> dbSettings)
        {
            _campaignsCollection = database.GetCollection<CampaignEntity>(dbSettings.Value.CampaignsCollectionName);
        }

        public async Task<CampaignEntity> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _campaignsCollection.FindOneAndDeleteAsync(p => p.Id == id, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete campaign with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<CampaignEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var campaigns = await _campaignsCollection.Find(_ => true).ToListAsync(cancellationToken);
                return campaigns;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve campaigns: {ex.Message}", ex);
            }
        }

        public async Task<CampaignEntity> GetDetailsAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var campaign = await _campaignsCollection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
                return campaign;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve campaign details with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<CampaignEntity> InsertAsync(CampaignEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _campaignsCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert campaign: {ex.Message}", ex);
            }
        }

        public async Task<CampaignEntity> UpdateAsync(CampaignEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<CampaignEntity>.Filter.Eq(p => p.Id, entity.Id);
                var result = await _campaignsCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
                if (result.ModifiedCount == 0)
                {
                    throw new Exception($"Failed to update campaign with id {entity.Id}: Campaign not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update campaign with id {entity.Id}: {ex.Message}", ex);
            }
        }
    }
}
