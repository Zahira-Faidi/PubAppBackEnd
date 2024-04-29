using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Persistence.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class PromotionRepository : ICommonRepository<PromotionEntity>
    {
        private readonly IMongoCollection<PromotionEntity> _promotionsCollection;

        public PromotionRepository(IMongoDatabase database, IOptions<DataBaseSettings> dbSettings)
        {
            _promotionsCollection = database.GetCollection<PromotionEntity>(dbSettings.Value.PromotionsCollectionName);
        }

        public async Task<PromotionEntity> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _promotionsCollection.FindOneAndDeleteAsync(p => p.Id == id, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete promotion with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<PromotionEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var promotions = await _promotionsCollection.Find(_ => true).ToListAsync(cancellationToken);
                return promotions;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve promotions: {ex.Message}", ex);
            }
        }

        public async Task<PromotionEntity> GetDetailsAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var promotion = await _promotionsCollection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
                return promotion;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve promotion details with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<PromotionEntity> InsertAsync(PromotionEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _promotionsCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert promotion: {ex.Message}", ex);
            }
        }

        public async Task<PromotionEntity> UpdateAsync(PromotionEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<PromotionEntity>.Filter.Eq(p => p.Id, entity.Id);
                var result = await _promotionsCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
                if (result.ModifiedCount == 0)
                {
                    throw new Exception($"Failed to update promotion with id {entity.Id}: Promotion not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update promotion with id {entity.Id}: {ex.Message}", ex);
            }
        }
    }
}
