namespace Authentication.Contracts.Authentication;

public record AuthenticationResponse
    (
        string Id,
        string FirstName,
        string LastName,
        string Email,
        string Role,
        string Image,
        string Status,
        string Token
    );