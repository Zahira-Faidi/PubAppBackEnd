using Authentication.Domain.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Authentication.Infrastructure.Security;

public class PasswordHash : IPasswordHasher
{
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        string newHashedPassword = HashPassword(password);
        return hashedPassword == newHashedPassword;
    }
}
