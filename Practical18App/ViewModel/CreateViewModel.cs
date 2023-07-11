using Practical18App.Models;
using System.ComponentModel.DataAnnotations;

namespace Practical18App.ViewModel
{
    public class CreateViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$", ErrorMessage = "Invalid phonenumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Please select Gender")]
        public Gender Gender { get; set; }
    }
}
