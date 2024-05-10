using Authentication.Domain.Entities;

namespace Authentication.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> RegisterUserAsync(string firstName, string lastName, string email, string password, string Role);
    Task<AuthenticationResult> Login(string email, string password);
}
