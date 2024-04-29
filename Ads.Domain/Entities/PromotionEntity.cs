using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ads.Domain.Entities
{
    public record PromotionEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; init; }
        public string? Description { get; init; }
        public double Discount { get; init; }
        public List<string>? Products { get; init; }
    }
}
