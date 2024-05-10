using Authentication.Application.Common.Interfaces.Authentication;
using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Application.Services.Authentication;
using Authentication.Domain.Interface;
using MediatR;

namespace Authentication.Application.Users.Queries.LoginQuery;

public class LoginQueryHandler : IRequestHandler<LoginQuery , AuthenticationResult>
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

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // Récupérer l'utilisateur à partir de l'email
        var user = await _userRepository.GetUserByEmailAsync(request.Email);
        // Vérifier si l'utilisateur existe et si le mot de passe est correct
        if (user == null || _passwordHasher.VerifyPassword(user.Password, request.Password))
        {
            return null; // Authentification échouée
        }
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}
