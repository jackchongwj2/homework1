using homework1.Models;

namespace homework1.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentDOB { get; set; }
        public string? StudentEnroll {  get; set; }

        public IEnumerable<ResultViewModel>? Result { get; set; }
    }

}
