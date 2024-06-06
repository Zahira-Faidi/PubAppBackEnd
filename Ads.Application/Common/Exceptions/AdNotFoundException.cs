namespace Ads.Application.Common.Exceptions;

public class AdNotFoundException : Exception
{
    public AdNotFoundException(string message) : base(message) { }
}
