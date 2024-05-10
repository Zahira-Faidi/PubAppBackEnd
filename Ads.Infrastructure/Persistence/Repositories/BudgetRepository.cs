using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Common;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class BudgetRepository : BaseRepository<BudgetEntity>, IBudgetRepository
    {
        public BudgetRepository(IMongoDatabase database) : base(database, "Budgets")
        {
        }
    }
}
