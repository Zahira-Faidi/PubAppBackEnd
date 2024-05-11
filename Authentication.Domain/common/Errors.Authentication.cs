using ErrorOr;

namespace Authentication.Domain.common;

public static partial class Errors

{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: "Auth.InvalidCred",
            description: "Invalid Credentials"
        );
    }
}
