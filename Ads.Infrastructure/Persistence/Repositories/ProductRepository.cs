using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Persistence.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : ICommonRepository<ProductEntity>
    {
        private readonly IMongoCollection<ProductEntity> _productsCollection;

        public ProductRepository(IMongoDatabase database, IOptions<DataBaseSettings> dbSettings)
        {
            _productsCollection = database.GetCollection<ProductEntity>(dbSettings.Value.ProductsCollectionName);
        }

        public async Task<ProductEntity> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _productsCollection.FindOneAndDeleteAsync(p => p.Id == id, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete product with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<ProductEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productsCollection.Find(_ => true).ToListAsync(cancellationToken);
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve products: {ex.Message}", ex);
            }
        }

        public async Task<ProductEntity> GetDetailsAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productsCollection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve product details with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<ProductEntity> InsertAsync(ProductEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _productsCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert product: {ex.Message}", ex);
            }
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<ProductEntity>.Filter.Eq(p => p.Id, entity.Id);
                var result = await _productsCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
                if (result.ModifiedCount == 0)
                {
                    throw new Exception($"Failed to update product with id {entity.Id}: Product not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update product with id {entity.Id}: {ex.Message}", ex);
            }
        }
    }
}
