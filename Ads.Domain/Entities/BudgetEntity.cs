using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class BudgetEntity: BaseEntity
{
    public double TotalBudget { get; set; }
    public double DailyBudget { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public List<string>? Campaigns { get; set; }
}
