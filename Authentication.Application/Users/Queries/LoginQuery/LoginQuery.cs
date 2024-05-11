using Authentication.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Queries.LoginQuery;

public record LoginQuery
(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
