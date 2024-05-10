using MediatR;

namespace Authentication.Application.Roles.Commands.AssignUserRoleCommand;

public record AssignUserRoleCommand(string UserId , string Role):IRequest;
