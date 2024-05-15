using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class AdEntity : BaseEntity
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? CampaignId { get; set; }
}
