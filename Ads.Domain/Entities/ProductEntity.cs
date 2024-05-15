using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class ProductEntity : BaseEntity
{
    public string? Name { get; set; }
    //public string? Description { get; set; }
    public string? Image { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? CategoryId { get; set; }
    //[BsonRepresentation(BsonType.ObjectId)]

    //public List<string>? Promotions { get; set; }
}