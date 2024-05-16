using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class BudgetEntity: BaseEntity
{
    [BsonElement("totalbudget")]
    public double TotalBudget { get; set; }
    [BsonElement("dailyBudget")]
    public double DailyBudget { get; set; }
}
