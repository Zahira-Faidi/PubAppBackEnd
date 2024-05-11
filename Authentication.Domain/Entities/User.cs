using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }

    private User(

    string firstname,
    string lastname,
    string email,
    string password,
    string role,
    DateTime createdDateTime,
    DateTime updatedDateTime

    )
    {

        FirstName = firstname;
        LastName = lastname;
        Email = email;
        Password = password;
        Role = role;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;

    }

    public static User Create(

        string firstname,
        string lastname,
        string email,
        string role,
        string password
    )
    {

        return new(

            firstname,
            lastname,
            email,
            password,
            role,
            DateTime.UtcNow,
            DateTime.UtcNow

        );
    }
}
