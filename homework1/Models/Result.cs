using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace homework1.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        [Column(TypeName = "numeric(4,1)")]
        [Range(0, 100, ErrorMessage = "Marks must be between 0 and 100.")]
        public double? Marks { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Please select a student.")]
        public int StudentId { get; set; }

        public Student? Student { get; set; }

        [Required(ErrorMessage = "Please select a subject.")]
        public int SubjectId { get; set; }

        public Subject? Subject { get; set; }
    }
}