using homework1.Models;

namespace homework1.Data.Interfaces
{
    public interface ITeacherRepository
    {
        Task<(IEnumerable<Teacher>, int)> GetTeachersIndexAsync(int pageNumber, int pageSize, string searchTerm);
        Task<IEnumerable<Teacher>> GetTeachersAsync();
        Task<IEnumerable<Teacher>> GetTeacherByIdAsync(int id);
        Task CreateTeacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
        Task<bool> TeacherExistsAsync(string email, string phoneNumber);
    }
}
