using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class BudgetEntity: BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("totalbudget")]
    public double TotalBudget { get; set; }
    [BsonElement("consumed")]
    public double Consumed { get; set; } = 0;
}
