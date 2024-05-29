using Authentication.Application.Common.Interfaces.Authentication;
using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Users.Common;
using Authentication.Domain.common;
using Authentication.Domain.Entities;
using Authentication.Domain.Interface;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand,ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmailAsync(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Image,
            request.Role,
            _passwordHasher.HashPassword(request.Password)
        ); 

        await _userRepository.AddAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}