using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class PromotionRepository : BaseRepository<PromotionEntity>, IPromotionRepository
    {
        public PromotionRepository(IMongoDatabase database) : base(database, "Promotions")
        {
        }
    }
}
