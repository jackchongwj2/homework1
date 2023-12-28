using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace homework1.ViewModels
{
    [Keyless]
    public class ResultViewModel
    {
        public int ResultId { get; set; }
        public double? Marks { get; set; }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentEmail { get ; set; }

        public int SubjectId { get; set; }
        public string? SubjectCode { get; set; }
        public string? SubjectName { get; set; }


        public string Grade
        {
            get
            {
                if (Marks == null)
                {
                    return "N/A";
                }
                else if (Marks >= 90)
                {
                    return "A+";
                }
                else if (Marks >= 80)
                {
                    return "A";
                }
                else if (Marks >= 70)
                {
                    return "B";
                }
                else if (Marks >= 60)
                {
                    return "C";
                }
                else if (Marks >= 50)
                {
                    return "D";
                }
                else
                {
                    return "F";
                }
            }
        }
    }
}
