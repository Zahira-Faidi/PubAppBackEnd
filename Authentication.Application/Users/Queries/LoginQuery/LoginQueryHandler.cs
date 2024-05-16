using Authentication.Application.Common.Interfaces.Authentication;
using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Users.Common;
using Authentication.Domain.common;
using Authentication.Domain.Entities;
using Authentication.Domain.Interface;
using ErrorOr;
using MediatR;

namespace Authentication.Application.Users.Queries.LoginQuery;

public class LoginQueryHandler : IRequestHandler<LoginQuery , ErrorOr<AuthenticationResult>>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public LoginQueryHandler(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByEmailAsync(request.Email) is not User user)

            return Errors.Authentication.InvalidCredentials;
       
        bool passwordValid = _passwordHasher.VerifyPassword(user.Password, request.Password);

        if (!passwordValid)
            return Errors.Authentication.InvalidCredentials;

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
