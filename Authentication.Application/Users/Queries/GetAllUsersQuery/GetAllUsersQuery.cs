using Authentication.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Queries.GetAllUsersQuery;

public class GetAllUsersQuery : IRequest<ErrorOr<List<UserResult>>>
{
}
