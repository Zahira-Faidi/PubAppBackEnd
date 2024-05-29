using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Users.Common;
using Authentication.Domain.Entities;
using Authentication.Domain.common;
using ErrorOr;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        var userResults = users.Select(u => u.ToUserResult()).ToList();

        if (!userResults.Any())
        {
            return Errors.User.UserNotFound;
        }

        return userResults;
    }
}
