using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homework1.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(8)")]
        [StringLength(30, ErrorMessage = "Code cannot exceed 8 characters.")]
        public string? Code { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Please select a teacher.")]
        public int TeacherId { get; set; }

        public Teacher? Teacher { get; set; }
        public List<Result>? Results { get; set; }
    }

}