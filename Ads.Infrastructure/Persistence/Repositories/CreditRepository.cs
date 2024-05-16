using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories;

public class CreditRepository : BaseRepository<CreditEntity>, ICreditRepository
{
    public CreditRepository(IMongoDatabase database) : base(database, "Credit")
    {

    }
}
