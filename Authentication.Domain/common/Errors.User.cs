using ErrorOr;

namespace Authentication.Domain.common;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email is already in use"
        );

        public static Error IdNotFound => Error.NotFound(
            code: "User.IdNotFound",
            description: "ID does not exist"
        );


        public static Error UserNotFound => Error.NotFound(
            code: "User.UserNotFound",
            description: "User does not exist"
        );


        public static Error UnexpectedError => Error.Unexpected(
            code: "User.UnexpectedError",
            description: "An unexpected error occurred"
        );
    }
}
