using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Domain.common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        if (id == null) return Errors.User.IdNotFound;

        var deletionSuccess = await _userRepository.DeleteUserAsync(id);

        if (!deletionSuccess) return Errors.User.UserNotFound;

        return Unit.Value;

    }
}