using Authentication.Application.Services.Authentication;
using MediatR;

namespace Authentication.Application.Users.Commands.RegisterUserCommand;

public record RegisterUserCommand
(
     string FirstName,
     string LastName,
     string Email,
     string Password,
     string Role
) : IRequest<AuthenticationResult>;