using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Practical17.Models
{
    public class Student
    {
        [Key]
        public int RollNo { get; set; }

        [NotMapped]
        public string? EncryptedRollNo { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max. length is 50")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "Max. length is 50")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max. length is 50")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }

        [Required]
        public int Standard { get; set; }

        public string Address { get; set; }
    }
}
