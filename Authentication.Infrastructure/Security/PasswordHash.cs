using Authentication.Domain.Interface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Authentication.Infrastructure.Security;

public class PasswordHash : IPasswordHasher
{
    public string HashPassword(string password)
    {
        // Générer un sel aléatoire
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hasher le mot de passe avec le sel généré
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000, // Le nombre d'itérations doit être choisi en fonction des capacités de votre système
            numBytesRequested: 256 / 8));

        // Retourner le sel et le hash sous forme de chaîne pour le stockage
        return $"{Convert.ToBase64String(salt)}:{hashed}";
    }

    public bool VerifyPassword(string hashedPasswordWithSalt, string passwordToCheck)
    {
        // Séparer le sel et le hash
        var parts = hashedPasswordWithSalt.Split(':');
        if (parts.Length != 2)
        {
            throw new FormatException("Le format du mot de passe hashé n'est pas valide.");
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hash = parts[1];

        // Hasher le mot de passe à vérifier avec le sel original
        string hashedToCheck = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: passwordToCheck,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        // Comparer les hash et retourner le résultat
        return hash == hashedToCheck;
    }
}