using System.ComponentModel.DataAnnotations;

namespace UserAuthenticationSystemV2.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Не указан Email")]
        [EmailAddress]
        [MaxLength(150)]
        [MinLength(5)]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Не указан пароль")]
        [MaxLength(150)]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
         
        [DataType(DataType.Password)]
        [MaxLength(150)]
        [MinLength(5)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}