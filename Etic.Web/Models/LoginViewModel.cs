using System.ComponentModel.DataAnnotations;

namespace Etic.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email Adresi Boş Bırakılamaz.")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Hatalı Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifre Giriniz."),MaxLength(12,ErrorMessage ="12 karakterden uzun olamaz.")]
        public string Password { get; set; }
    }
}
