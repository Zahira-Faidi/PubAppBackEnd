using Ads.Application.Common.Base;
using Ads.Domain.Common.Entities;
using MongoDB.Driver;

namespace Ads.Infrastructure.Common
{

    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {


        private readonly IMongoCollection<T> _collection;
        public BaseRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);

        }
        public async Task<T> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.FindOneAndDeleteAsync(filter, cancellationToken: cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _collection.Find(_ => true).ToListAsync(cancellationToken);
        }

        public async Task<T> GetDetailsAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> InsertAsync(T entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public async Task<T> UpdateAsync(string id, T entity, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var options = new ReplaceOptions { IsUpsert = false };
            await _collection.ReplaceOneAsync(filter, entity, options, cancellationToken);
            return entity;
        }
    }
}
