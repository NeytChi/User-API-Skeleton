using System.Text;
using System.Security.Cryptography;

namespace Skeleton.Services
{
    public static class PasswordHelper
    {
        private static byte[] _salt;

        public static byte[] storedSalt
        {
            get 
            { 
                if (_salt is null)
                {
                    _salt = GenerateSalt();
                }
                return _salt; 
            }
        }
        public static byte[] GenerateSalt(int length = 16)
        {
            var salt = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public static byte[] HashPasswordWithSalt(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var saltedPasswordBytes = new byte[passwordBytes.Length + storedSalt.Length];

                Buffer.BlockCopy(passwordBytes, 0, saltedPasswordBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(storedSalt, 0, saltedPasswordBytes, passwordBytes.Length, storedSalt.Length);

                return sha256.ComputeHash(saltedPasswordBytes);
            }
        }
        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            var enteredHash = HashPasswordWithSalt(enteredPassword);

            var hashFromBase64 = FromBase64String(storedHash);

            return enteredHash.SequenceEqual(hashFromBase64);
        }
        public static byte[] FromBase64String(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
        public static string ToBase64String(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }
        public static string GetPasswordForStore(string enteredPassword)
        {
            var enteredHash = HashPasswordWithSalt(enteredPassword);

            return ToBase64String(enteredHash);
        }
    }
}