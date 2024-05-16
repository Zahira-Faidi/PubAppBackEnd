using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class AdEntity : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }
    [BsonElement("startDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset StartDate { get; set; } = DateTime.UtcNow;
    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset EndDate { get; set; } = DateTime.UtcNow;
    [BsonElement("campaignId")] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CampaignId { get; set; }
    [BsonElement("creditId")]
    public string? CreditId { get; set; }
}
