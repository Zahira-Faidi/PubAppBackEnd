using Authentication.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Commands.UpdateUserCommand;

public record UpdateUserCommand(
    string Id,
    string FirstName,
    string LastName,
    string Image,
    string Status
) : IRequest<ErrorOr<UserResult>>;
