using Ads.Domain.Common.Entities;
using Ads.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
namespace Ads.Domain.Entities;

public class CampaignEntity : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }
    [BsonElement("startDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset StartDate { get; set; } = DateTime.UtcNow;
    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset EndDate { get; set; } = DateTime.UtcNow;
    [BsonElement("impressions")]
    public int Impressions { get; set; }
    [BsonElement("seller")]
    public string? SellerId { get; set; }
    [BsonElement("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public Status Status { get; set; }
    [BsonElement("budget")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? BudgetId { get; set; }
    [BsonElement("consumed")]
    public double Consumed { get; set; } = 0;
}
