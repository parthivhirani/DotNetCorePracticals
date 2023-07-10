using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Practical17.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max. length is 50")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max. length is 50")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid email address!")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [StringLength(10, ErrorMessage = "Invalid contact number!")]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(13, ErrorMessage = "Password length can't be more than 13")]
        [MinLength(6, ErrorMessage = "Password length can't be less than 6")]
        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
