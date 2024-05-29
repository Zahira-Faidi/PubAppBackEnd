using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Users.Common;
using Authentication.Domain.common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Queries.GetUserByIdQuery;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResult>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            return Errors.User.IdNotFound;
        }
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            return Errors.User.IdNotFound;
        }
        var userResult = user.ToUserResult();
        return userResult;
    }
}