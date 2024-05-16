using Authentication.Domain.Entities;

namespace Authentication.Application.Users.Common;

public record AuthenticationResult
(
    User User,
    string Token
);
