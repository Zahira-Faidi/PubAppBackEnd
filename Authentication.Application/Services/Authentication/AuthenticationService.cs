using Authentication.Application.Common.Errors;
using Authentication.Application.Common.Interfaces.Authentication;
using Authentication.Application.Common.Interfaces.Persistence;
using Authentication.Domain.Entities;
using Authentication.Domain.Interface;

namespace Authentication.Application.Services.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHashService;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHashService = passwordHasher;
    }

    public async Task<AuthenticationResult> Login(string email, string password)
    {
        // 1. Validate the user exists
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User with given email does not exist");
        }

        // 2. Validate the password is correct
        if (!_passwordHashService.VerifyPassword(user.Password, password))
        {
            throw new Exception("Invalid password");
        }

        // 3. Create Jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }

    public async Task<AuthenticationResult> RegisterUserAsync(string firstName, string lastName, string email, string password, string role)
    {
        // 1. Validate the user doesn't exist
        var existingUser = await _userRepository.GetUserByEmailAsync(email);
        if (existingUser != null)
        {
            throw new DuplicateEmailException();
        }

        string hashedPassword = _passwordHashService.HashPassword(password);

        // 2. Create user (generate unique Id) & Persist to DB
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Role = role ?? Role.Client,
            Password = hashedPassword
        };

        await _userRepository.AddAsync(user);

        // 3. Create Jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

}