using Ads.Domain.Common.Entities;

namespace Ads.Domain.Entities;

public class PromotionEntity : BaseEntity
{
    public string? Description { get; set; }
    public double Discount { get; set; }
    public List<string>? Products { get; set; }
}
