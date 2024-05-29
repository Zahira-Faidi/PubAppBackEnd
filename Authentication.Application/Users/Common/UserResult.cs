using Authentication.Domain.Entities;

namespace Authentication.Application.Users.Common;

public record UserResult(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Image,
    string Status
);

public record UsersResult(IEnumerable<UserResult> Users);

public static class UserResultExtensions
{
    public static UserResult ToUserResult(this User user)
    {
        return new UserResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role,
            user.CreatedDateTime,
            user.UpdatedDateTime,
            user.Image,
            user.Status
        );
    }
}
