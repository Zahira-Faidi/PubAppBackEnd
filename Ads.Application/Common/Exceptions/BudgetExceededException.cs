namespace Ads.Application.Common.Exceptions
{
    public class BudgetExceededException : Exception
    {
        public BudgetExceededException(string message) : base(message)
        {
        }
    }
}
