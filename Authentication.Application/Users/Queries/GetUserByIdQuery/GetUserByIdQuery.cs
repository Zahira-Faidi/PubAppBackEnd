using Authentication.Application.Users.Common;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Queries.GetUserByIdQuery;

public class GetUserByIdQuery : IRequest<ErrorOr<UserResult>>
{
    public string Id { get; set; }
}
