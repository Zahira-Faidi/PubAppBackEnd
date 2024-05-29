using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Users.Common;
using Authentication.Domain.Entities;
using Authentication.Domain.common;
using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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

        // Update the user fields if they are provided
        user.FirstName = request.FirstName ?? user.FirstName;
        user.LastName = request.LastName ?? user.LastName;
        user.Image = request.Image ?? user.Image;
        user.Status = request.Status ?? user.Status;
        user.UpdatedDateTime = DateTime.UtcNow;

        var updateResult = await _userRepository.UpdateAsync(user);

        if (!updateResult) return Errors.User.UnexpectedError;

        // Return the updated user as UserResult
        var userResult = new UserResult(
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

        return userResult;
    }
}
