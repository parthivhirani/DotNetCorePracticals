using System.ComponentModel.DataAnnotations;

namespace Practical17.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
