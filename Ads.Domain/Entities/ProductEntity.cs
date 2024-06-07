using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Ads.Domain.Common.Entities;

namespace Ads.Domain.Entities
{
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

        [BsonElement("click")]
        public int Click { get; set; } = 0;

        [BsonElement("category")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }

        [BsonElement("ad")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AdId { get; set; }

        [BsonElement("clickHistory")]
        public List<ClickEvent> ClickHistory { get; set; } = new List<ClickEvent>();
    }

    public class ClickEvent
    {
        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("clickCount")]
        public int ClickCount { get; set; }
    }
}
