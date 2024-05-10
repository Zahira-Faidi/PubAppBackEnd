using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories;

public class AdRepository : BaseRepository<AdEntity>, IAdRepository
{
    public AdRepository(IMongoDatabase database) : base(database, "Ads")
    {
    }
}
