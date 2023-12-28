using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homework1.Models
{
    public class Teacher
    {
        // By convention, a property named Id or <type name>Id will be configured as the primary key of an entity.
        public int TeacherId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain numbers.")]
        public string? Name { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Email cannot exceed 30 characters.")]
        [Column(TypeName = "varchar(30)")]
        [CustomEmailFormat] // Use your custom email validation attribute
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        [CustomPhoneNumberFormat]
        public string? PhoneNumber { get; set; }

        public List<Subject>? Subjects { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        
    }

    public class CustomEmailFormatAttribute : RegularExpressionAttribute
    {
        public CustomEmailFormatAttribute() : base(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
        {
            ErrorMessage = "Invalid email format";
        }
    }
    public class CustomPhoneNumberFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string phoneNumber = value.ToString();

                // Remove symbols (+, -) and check the length
                string cleanPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

                if (cleanPhoneNumber.Length >= 10 && cleanPhoneNumber.Length <= 11)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Invalid phone number format. It should have a minimum of 10 and a maximum of 11 digits without symbols.");
                }
            }

            return ValidationResult.Success; // If the value is null, assume it's valid (you can adjust this according to your requirements)
        }
    }
}