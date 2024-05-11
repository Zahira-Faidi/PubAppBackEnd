using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Users.Common;
using Authentication.Domain.common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserResult>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;
        if (id == null) return Errors.User.IdNotFound;

        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return Errors.User.UserNotFound;
        }

        user.FirstName = request.FirstName ?? user.FirstName;
        user.LastName = request.LastName ?? user.LastName;

        var updatedUser = await _userRepository.UpdateAsync(user);

        if (!updatedUser) return Errors.User.UnexpectedError;

        return new UserResult(user.Id, user.FirstName, user.LastName, user.Email, user.Role, user.CreatedDateTime, user.UpdatedDateTime);
    }
}