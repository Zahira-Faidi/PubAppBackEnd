namespace Ads.Api.Common.Utils
{
    public static class ValidationUtils
    {
        public static bool IsValidId(string id)
        {
            return !string.IsNullOrEmpty(id) && id.Length == 24 && id.All(char.IsLetterOrDigit);
        }
    }
}
