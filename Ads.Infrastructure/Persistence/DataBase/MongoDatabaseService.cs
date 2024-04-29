using Ads.Infrastructure.Persistence.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence
{
    public class MongoDatabaseService
    {
        private readonly IMongoDatabase _database;

        public MongoDatabaseService(IOptions<DataBaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
