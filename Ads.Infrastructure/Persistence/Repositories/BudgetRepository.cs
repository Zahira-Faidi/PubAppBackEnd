using Ads.Application.Common.Interfaces;
using Ads.Domain.Entities;
using Ads.Infrastructure.Persistence.DataBase;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ads.Infrastructure.Persistence.Repositories
{
    public class BudgetRepository : ICommonRepository<BudgetEntity>
    {
        private readonly IMongoCollection<BudgetEntity> _budgetsCollection;

        public BudgetRepository(IMongoDatabase database, IOptions<DataBaseSettings> dbSettings)
        {
            _budgetsCollection = database.GetCollection<BudgetEntity>(dbSettings.Value.BudgetsCollectionName);
        }

        public async Task<BudgetEntity> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _budgetsCollection.FindOneAndDeleteAsync(p => p.Id == id, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete budget with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<List<BudgetEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            try
            {
                var budgets = await _budgetsCollection.Find(_ => true).ToListAsync(cancellationToken);
                return budgets;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve budgets: {ex.Message}", ex);
            }
        }

        public async Task<BudgetEntity> GetDetailsAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var budget = await _budgetsCollection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
                return budget;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve budget details with id {id}: {ex.Message}", ex);
            }
        }

        public async Task<BudgetEntity> InsertAsync(BudgetEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                await _budgetsCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert budget: {ex.Message}", ex);
            }
        }

        public async Task<BudgetEntity> UpdateAsync(BudgetEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var filter = Builders<BudgetEntity>.Filter.Eq(p => p.Id, entity.Id);
                var result = await _budgetsCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
                if (result.ModifiedCount == 0)
                {
                    throw new Exception($"Failed to update budget with id {entity.Id}: Budget not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update budget with id {entity.Id}: {ex.Message}", ex);
            }
        }
    }
}
