using Etic.Business.Helpers;
using Etic.Data.Abstract;
using Etic.Entities;

namespace Etic.Business.Services
{
    /// <summary>
    /// Kullanıcı giriş servisi
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly IUserDal _userDal;
        
        public LoginService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        /// <summary>
        /// Kullanıcı girişi yapar
        /// </summary>
        /// <param name="email">Email adresi</param>
        /// <param name="password">Düz metin şifre</param>
        /// <returns>Kullanıcı bulunduysa User, bulunamadıysa null</returns>
        public User Login(string email, string password)
        {
            // Kullanıcıyı email'e göre bul
            var user = _userDal.Get(x => x.Email == email);
            
            if (user == null)
            {
                return null; // Kullanıcı bulunamadı
            }

            // Şifre doğrulama
            // NOT: Veritabanında düz şifre varsa direkt karşılaştır (geçici)
            // Hash'li şifre varsa PasswordHasher.VerifyPassword kullan
            
            bool isPasswordCorrect = false;
            
            // Eğer veritabanındaki şifre hash'li değilse (düz metin)
            if (user.PasswordHash == password)
            {
                isPasswordCorrect = true;
                // TODO: İlk girişte düz şifreyi hash'le ve güncelle
            }
            // Eğer veritabanındaki şifre hash'li ise
            else if (PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                isPasswordCorrect = true;
            }

            return isPasswordCorrect ? user : null;
        }
    }
}
