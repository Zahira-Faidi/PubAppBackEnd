using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Persistence.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICommonRepository<CategoryEntity>
    {
        private readonly IMongoCollection<CategoryEntity> _categoriesCollection;

        public CategoryRepository(IMongoDatabase database, IOptions<DataBaseSettings> dbSettings)
        {
            _categoriesCollection = database.GetCollection<CategoryEntity>(dbSettings.Value.CategoriesCollectionName);
        }

        public async Task<CategoryEntity> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _categoriesCollection.FindOneAndDeleteAsync(c => c.Id == id, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete category with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<CategoryEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _categoriesCollection.Find(_ => true).ToListAsync(cancellationToken);
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve categories: {ex.Message}", ex);
            }
        }

        public async Task<CategoryEntity> GetDetailsAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoriesCollection.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve category details with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<CategoryEntity> InsertAsync(CategoryEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _categoriesCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert category: {ex.Message}", ex);
            }
        }

        public async Task<CategoryEntity> UpdateAsync(CategoryEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<CategoryEntity>.Filter.Eq(c => c.Id, entity.Id);
                var result = await _categoriesCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
                if (result.ModifiedCount == 0)
                {
                    throw new Exception($"Failed to update category with id {entity.Id}: Category not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update category with id {entity.Id}: {ex.Message}", ex);
            }
        }
    }
}
