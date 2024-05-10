using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class AdEntity : BaseEntity
{
    public string? Content { get; set; }
    public double AllocatedBudget { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? CampaignId { get; set; }
}
