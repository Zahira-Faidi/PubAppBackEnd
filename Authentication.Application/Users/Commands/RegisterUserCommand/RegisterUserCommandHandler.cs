using Authentication.Application.Services.Authentication;
using Authentication.Domain.Interface;
using MediatR;

namespace Authentication.Application.Users.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticationResult>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IPasswordHasher _passwordHasher;
    public RegisterUserCommandHandler(IAuthenticationService authenticationService, IPasswordHasher passwordHasher)
    {
        _authenticationService = authenticationService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user =  await _authenticationService.RegisterUserAsync(request.FirstName, request.LastName, request.Email, _passwordHasher.HashPassword(request.Password), request.Role);
        return user;
    }
}