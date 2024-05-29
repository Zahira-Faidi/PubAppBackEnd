using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;

namespace Ads.Domain.Entities;

public class ProductEntity : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }
    [BsonElement("image")]
    public string? Image { get; set; }
    [BsonElement("price")]
    public double Price { get; set; }
    [BsonElement("quantity")]
    public int Quantity { get; set; }
    [BsonElement("cpc")]
    public int CPC { get; set; } = 0;
    [BsonElement("category")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CategoryId { get; set; }
    [BsonElement("ad")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? AdId { get; set; }
}