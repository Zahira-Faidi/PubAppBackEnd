using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories;

public class AdRepository : BaseRepository<AdEntity>, IAdRepository
{
    private readonly IMongoDatabase _database;

    public AdRepository(IMongoDatabase database) : base(database, "Ads")
    {
        _database = database;

    }

    public async Task<List<AdEntity>> GetAllAdsByCampaignId(string campaingId, CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<AdEntity>("Ads");

        var filter = Builders<AdEntity>.Filter.Eq(x => x.CampaignId, campaingId);

        return await collection.Find(filter).ToListAsync();
    }
}
