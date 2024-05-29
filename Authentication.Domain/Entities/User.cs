using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Image")]
        public string Image { get; set; }

        [BsonElement("Status")]
        public string Status { get; set; } = "Active";  // Default status value

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        [BsonElement("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Role")]
        public string Role { get; set; }

        [BsonElement("CreatedDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [BsonElement("UpdatedDateTime")]
        public DateTime UpdatedDateTime { get; set; }

        private User(
            string firstName,
            string lastName,
            string email,
            string image,
            string role,
            string password,
            DateTime createdDateTime,
            DateTime updatedDateTime)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Image = image;
            Status = "Active"; // Initialize status with default value
            Role = role;
            Password = password;
            CreatedDateTime = createdDateTime;
            UpdatedDateTime = updatedDateTime;
        }

        public static User Create(
            string firstName,
            string lastName,
            string email,
            string image,
            string role,
            string password)
        {
            return new User(
                firstName,
                lastName,
                email,
                image,
                role,
                password,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }
    }
}
