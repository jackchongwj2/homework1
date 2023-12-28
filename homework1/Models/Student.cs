using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homework1.Models
{
    public class Student
    {
        // By convention, a property named Id or <type name>Id will be configured as the primary key of an entity.
        public int StudentId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain numbers.")]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(30)")]
        [StringLength(30, ErrorMessage = "Email cannot exceed 30 characters.")]
        [CustomEmailFormat]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public List<Result>? Results { get; set; }
    }

}
