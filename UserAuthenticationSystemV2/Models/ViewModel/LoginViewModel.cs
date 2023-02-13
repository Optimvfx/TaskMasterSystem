using System.ComponentModel.DataAnnotations;

namespace UserAuthenticationSystemV2.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        [EmailAddress]
        [MaxLength(150)]
        [MinLength(5)]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public LoginViewModel()
        {
            
        }
    }
}