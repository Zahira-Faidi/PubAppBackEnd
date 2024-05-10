using System.ComponentModel.DataAnnotations;

namespace MongoDBIdentity.Models.ViewModels
{
    public class UserModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
