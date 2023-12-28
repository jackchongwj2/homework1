using homework1.Data.Interfaces;
using homework1.Data.Repositories;
using homework1.Models;

namespace homework1.Data.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<(IEnumerable<Student>, int)> GetStudentsIndexAsync(
            int pageNumber, int pageSize, string searchTerm)
        {
            return await _studentRepository.GetStudentsIndexAsync(pageNumber, pageSize, searchTerm);
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetStudentByIdAsync(id);
        }

        public async Task CreateStudentAsync(Student student)
        {
            await _studentRepository.CreateStudentAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateStudentAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepository.DeleteStudentAsync(id);
        }

        public async Task<bool> StudentExistsAsync(string email)
        {
            return await _studentRepository.StudentExistsAsync(email);
        }
    }
}
