using Ads.Domain.Common.Entities;

namespace Ads.Domain.Entities;

public class CreditEntity : BaseEntity
{
    public string? Name { get; set; }
    public double AvailableCredit { get; set; }
    public double Consumed { get; set; }

}
