using Authentication.Application.Services.Authentication;
using MediatR;

namespace Authentication.Application.Users.Queries.LoginQuery;

public record LoginQuery
(
    string Email,
    string Password
) : IRequest<AuthenticationResult>;
