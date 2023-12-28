using homework1.Data.Interfaces;
using homework1.Models;

namespace homework1.Data.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<(IEnumerable<Teacher>, int)> GetTeachersIndexAsync(
            int pageNumber, int pageSize, string searchTerm)
        {
            return await _teacherRepository.GetTeachersIndexAsync(pageNumber, pageSize, searchTerm);
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await _teacherRepository.GetTeachersAsync();
        }

        public async Task<IEnumerable<Teacher>> GetTeacherByIdAsync(int id)
        {
            return await _teacherRepository.GetTeacherByIdAsync(id);
        }

        public async Task CreateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.CreateTeacherAsync(teacher);
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            await _teacherRepository.UpdateTeacherAsync(teacher);
        }

        public async Task DeleteTeacherAsync(int id)
        {
            await _teacherRepository.DeleteTeacherAsync(id);
        }

        public async Task<bool> TeacherExistsAsync(string email, string phoneNumber)
        {
            return await _teacherRepository.TeacherExistsAsync(email, phoneNumber);
        }
    }
}
