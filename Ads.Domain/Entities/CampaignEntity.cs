using Ads.Domain.Common.Entities;
using Ads.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace Ads.Domain.Entities;

public class CampaignEntity : BaseEntity
{
    public string? Name { get; set; }
    //public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    //public double Budget { get; set; }
    public Status Status { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]

    public List<string>? Ads { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]

    public string? BudgetId { get; set; }
}
