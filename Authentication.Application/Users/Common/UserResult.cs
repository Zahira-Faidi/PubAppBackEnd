namespace Authentication.Application.Users.Common;

public record UserResult
(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record UsersResult(IEnumerable<UserResult> Users);
