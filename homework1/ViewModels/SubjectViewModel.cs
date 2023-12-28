using Microsoft.EntityFrameworkCore;

namespace homework1.ViewModels
{
    [Keyless]
    public class SubjectViewModel
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }

        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
    }

}
