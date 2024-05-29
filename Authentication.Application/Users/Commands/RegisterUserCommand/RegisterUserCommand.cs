using Authentication.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Commands.RegisterUserCommand;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Image,
    string Role,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
