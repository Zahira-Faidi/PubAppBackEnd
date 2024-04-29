using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Ads.Domain.Entities
{
    public class BudgetEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public double TotalBudget { get; set; }
        public double DailyBudget { get; set; }
        public List<string>? Campaigns { get; set; }
    }
}
