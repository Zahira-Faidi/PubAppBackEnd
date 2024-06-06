namespace Ads.Application.Common.Exceptions;

public class BudgetNotFoundException: Exception
{
    public BudgetNotFoundException(string message) : base(message)
    {

    }
}
