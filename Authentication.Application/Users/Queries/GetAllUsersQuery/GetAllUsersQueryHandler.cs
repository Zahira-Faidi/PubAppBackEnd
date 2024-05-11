using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Users.Common;
using Authentication.Domain.common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Queries.GetAllUsersQuery;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<List<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<UserResult>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsersAsync();
        var userResult = users.Select(u => new UserResult(u.Id, u.FirstName, u.LastName, u.Email, u.Role, u.CreatedDateTime, u.UpdatedDateTime)).ToList();

        if (userResult.Count == 0) return Errors.User.UserNotFound;

        return userResult;

    }
}