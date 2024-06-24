using Authentication.Domain.Entities;

namespace Authentication.Application.Services.Authentication;

public record AuthenticationResult
(
    User User,
    string Token
);
