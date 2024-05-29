namespace Authentication.Contracts.User;

public record UserResponse
(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string Image,
    string Status
);
