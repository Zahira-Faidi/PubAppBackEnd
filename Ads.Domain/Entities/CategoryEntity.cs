using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Ads.Domain.Entities;

public class CategoryEntity : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }
}
