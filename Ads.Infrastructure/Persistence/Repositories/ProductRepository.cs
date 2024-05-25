using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        private readonly IMongoDatabase _database;
        public ProductRepository(IMongoDatabase database) : base(database, "Products")
        {
            _database = database;
        }

        public async Task<List<ProductEntity>> GetAllProductsByAdId(string adId, CancellationToken cancellationToken)
        {
            var collection = _database.GetCollection<ProductEntity>("Products");

            var filter = Builders<ProductEntity>.Filter.Eq(x => x.AdId, adId);

            return await collection.Find(filter).ToListAsync();
        }
    }
}
