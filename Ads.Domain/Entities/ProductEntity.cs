using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Ads.Domain.Entities
{
    public class ProductEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? CategoryId { get; set; }
        public List<string>? Promotions { get; set; }
    }
}
