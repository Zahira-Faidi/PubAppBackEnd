using Authentication.Domain.Entities;

namespace Authentication.Contracts.Authentication;

public record RegisterRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string Image,
        string Role,
        string Password
    );
