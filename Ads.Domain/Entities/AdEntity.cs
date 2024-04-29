using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ads.Domain.Entities
{
    public class AdEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Content { get; set; }
        public double AllocatedBudget { get; set; }
        public string? CampaignId { get; set; }
    }
}
