namespace Ads.Application.Common.Exceptions;

public class CampaignNotFoundException : Exception
{
    public CampaignNotFoundException(string message) : base(message)
    {
    }
}
