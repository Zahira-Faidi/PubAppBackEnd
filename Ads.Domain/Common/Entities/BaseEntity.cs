using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Common.Entities;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement("created")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    [BsonElement("updated")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
