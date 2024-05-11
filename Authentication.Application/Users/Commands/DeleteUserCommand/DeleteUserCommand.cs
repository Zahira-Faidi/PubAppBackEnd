using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Commands.DeleteUserCommand;

public class DeleteUserCommand : IRequest<ErrorOr<Unit>>
{
    public string Id { get; set; }
}
