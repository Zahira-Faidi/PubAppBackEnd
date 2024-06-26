﻿using Ads.Domain.Common.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ads.Domain.Entities;

public class AdEntity : BaseEntity
{
    [BsonElement("name")]
    public string? Name { get; set; }
    [BsonElement("startDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset StartDate { get; set; } = DateTime.UtcNow;
    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset EndDate { get; set; } = DateTime.UtcNow;
    [BsonElement("campaignId")] 
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CampaignId { get; set; }
    [BsonElement("credit")]
    public double Credit { get; set; } = 0; 
    [BsonElement("consumed")]
    public double Consumed { get; set; } = 0;
    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; } = false;
    // la somme des clicks des produits d'une même annonce
    [BsonElement("impressions")]
    public int Impressions { get; set; } = 0;

}
