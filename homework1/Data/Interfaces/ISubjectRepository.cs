using homework1.Models;
using homework1.ViewModels;

namespace homework1.Data.Interfaces
{
    public interface ISubjectRepository
    {
        Task<(IEnumerable<Subject>, int)> GetSubjectsIndexAsync(int pageNumber, int pageSize, string searchTerm);
        Task<IEnumerable<Subject>> GetSubjectsAsync();
        Task<IEnumerable<Subject>> GetSubjectByIdAsync(int id);
        Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Subject>> GetAvailableSubjectsByStudentIdAsync(int studentId);
        Task CreateSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int id);
        Task<bool> SubjectExistsAsync(string name, string code);
    }
}
