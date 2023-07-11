using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Practical18API.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^([a-zA-z ]){1,50}", ErrorMessage = "Name only contains the Alphabets")]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Invalid Phonenumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Please Select Gender")]
        public Gender Gender { get; set; }
    }
}
