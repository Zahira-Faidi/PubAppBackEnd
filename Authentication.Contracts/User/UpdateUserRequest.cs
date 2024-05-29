namespace Authentication.Contracts.User;

public record UpdateUserRequest
(
    string FirstName,
    string LastName,
    string Status,
    string Image
);
