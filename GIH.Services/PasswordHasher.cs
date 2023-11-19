using System.Security.Cryptography;
using System.Text;

namespace GIH.Services;

public class PasswordHasher
{
    private const int SaltSize = 32;
    public static (string Hash, string Salt) HashPassword(string password)
    {
       // Rastgele bir tuz oluşturun
        byte[] salt = GenerateSalt();

        // Şifreyi ve tuzu birleştirip SHA-256 ile hash'e dönüştürün
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];
        Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(combinedBytes);

            // Hash'i string olarak dönüştür
            string hash = BytesToHexString(hashBytes);
            string saltString = Convert.ToBase64String(salt);

            return (hash, saltString);
        }
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        // Girilen şifreyi ve kaydedilmiş tuzu birleştirip SHA-256 ile hash'e dönüştürülür
        byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
        byte[] storedSaltBytes = Convert.FromBase64String(storedSalt);
        byte[] combinedBytes = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
        Buffer.BlockCopy(enteredPasswordBytes, 0, combinedBytes, 0, enteredPasswordBytes.Length);
        Buffer.BlockCopy(storedSaltBytes, 0, combinedBytes, enteredPasswordBytes.Length, storedSaltBytes.Length);

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] enteredHashBytes = sha256.ComputeHash(combinedBytes);

            // Hash'leri karşılaştır
            return BytesToHexString(enteredHashBytes) == storedHash;
        }
    }

    private static byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            return salt;
        }
    }

    private static string BytesToHexString(byte[] bytes)
    {
        StringBuilder builder = new StringBuilder();
        foreach (byte b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}