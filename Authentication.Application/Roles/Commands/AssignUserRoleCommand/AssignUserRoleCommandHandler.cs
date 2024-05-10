using Authentication.Application.Common.Interfaces.Persistence;
using MediatR;

namespace Authentication.Application.Roles.Commands.AssignUserRoleCommand;

public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
{
    private readonly IUserRepository _userRepository;

    public AssignUserRoleCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
            throw new Exception("User not found");

        // Assign role
        user.Role = request.Role ?? user.Role;
        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}
