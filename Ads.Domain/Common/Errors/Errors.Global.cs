using ErrorOr;
namespace Ads.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Global
        {
            public static Error IdNotFound =>
                Error.Conflict(

                    code: "Global.IdNotFound",
                    description: "Id not found for this"
                    );
        }
    }
}
