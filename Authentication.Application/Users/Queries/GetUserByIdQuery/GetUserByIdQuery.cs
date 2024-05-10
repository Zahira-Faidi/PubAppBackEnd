using Authentication.Domain.Entities;
using MediatR;

namespace Authentication.Application.Users.Queries.GetUserByIdQuery;

public class GetUserByIdQuery : IRequest<User>
{
    public string UserId { get; set; }
}
