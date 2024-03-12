using System.Security.Cryptography;
using System.Text;
using Domestica.Budget.Application.Abstractions.Authentication;

namespace Domestica.Budget.Infrastructure.Authentication
{
    public sealed class PasswordService : IPasswordService
    {
        private const byte keySize = 32;
        private const byte saltSize = 16;
        private const int iterations = 3500000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private const char delimiter = ';';

        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(saltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return string.Join(delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyPassword(string passwordInput, string passwordHash)
        {
            var elements = passwordHash.Split(delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(
                passwordInput,
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
