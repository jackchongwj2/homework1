using homework1.Models;

namespace homework1.Data.Interfaces
{
    public interface IStudentRepository
    {
        Task<(IEnumerable<Student>, int)> GetStudentsIndexAsync(int pageNumber, int pageSize, string searchTerm);
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<IEnumerable<Student>> GetStudentByIdAsync(int id);
        Task CreateStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<bool> StudentExistsAsync(string email);
    }
}