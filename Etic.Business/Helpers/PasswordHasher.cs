using System;
using System.Security.Cryptography;
using System.Text;

namespace Etic.Business.Helpers
{
    /// <summary>
    /// Şifre hash'leme ve doğrulama için yardımcı sınıf
    /// SHA256 algoritması kullanır
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Şifreyi hash'ler (SHA256)
        /// </summary>
        /// <param name="password">Düz metin şifre</param>
        /// <returns>Hash'lenmiş şifre</returns>
        public static string HashPassword(string password)
        {
            // SHA256 hash algoritması oluştur
            using (SHA256 sha256 = SHA256.Create())
            {
                // Şifreyi byte dizisine çevir
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                
                // Hash'le
                byte[] hash = sha256.ComputeHash(bytes);
                
                // Byte dizisini hex string'e çevir
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2")); // x2 = hexadecimal 2 digit
                }
                
                return builder.ToString();
            }
        }

        /// <summary>
        /// Şifreyi doğrular (hash'ler ve karşılaştırır)
        /// </summary>
        /// <param name="password">Düz metin şifre</param>
        /// <param name="hashedPassword">Hash'lenmiş şifre</param>
        /// <returns>True = doğru şifre, False = yanlış şifre</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Girilen şifreyi hash'le
            string hashOfInput = HashPassword(password);
            
            // Karşılaştır (case-insensitive)
            return hashOfInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}

