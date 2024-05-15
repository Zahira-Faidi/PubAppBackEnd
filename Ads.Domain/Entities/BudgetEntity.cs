using Ads.Domain.Common.Entities;

namespace Ads.Domain.Entities;

public class BudgetEntity: BaseEntity
{
    public double TotalBudget { get; set; }
    public double DailyBudget { get; set; }
}
