namespace Authentication.Contracts.User;

public record UserRequest(

    string FirstName,
    string LastName,
    string Email,
    string Role
);
