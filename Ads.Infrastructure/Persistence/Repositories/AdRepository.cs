using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Persistence.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class AdRepository : ICommonRepository<AdEntity>
    {
        private readonly IMongoCollection<AdEntity> _AdsCollection;

        public AdRepository(IMongoDatabase database, IOptions<DataBaseSettings> dbSettings)
        {
            _AdsCollection = database.GetCollection<AdEntity>(dbSettings.Value.AdsCollectionName);
        }

        public async Task<AdEntity> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _AdsCollection.FindOneAndDeleteAsync(p => p.Id == id, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete ad with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<AdEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _AdsCollection.Find(_ => true).ToListAsync(cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve ads: {ex.Message}", ex);
            }
        }

        public async Task<AdEntity> GetDetailsAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _AdsCollection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve ad details with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<AdEntity> InsertAsync(AdEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _AdsCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert ad: {ex.Message}", ex);
            }
        }

        public async Task<AdEntity> UpdateAsync(AdEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<AdEntity>.Filter.Eq(p => p.Id, entity.Id);
                var result = await _AdsCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
                if (result.ModifiedCount == 0)
                {
                    throw new Exception($"Failed to update ad with id {entity.Id}: Ad not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update ad with id {entity.Id}: {ex.Message}", ex);
            }
        }
    }
}
