using homework1.Data.Interfaces;
using homework1.Data.Repositories;
using homework1.Models;
using homework1.ViewModels;

namespace homework1.Data.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<(IEnumerable<Subject>, int)> GetSubjectsIndexAsync(
            int pageNumber, int pageSize, string searchTerm)
        {
            return await _subjectRepository.GetSubjectsIndexAsync(pageNumber, pageSize, searchTerm);
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            return await _subjectRepository.GetSubjectsAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectByIdAsync(int id)
        {
            return await _subjectRepository.GetSubjectByIdAsync(id);
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            return await _subjectRepository.GetSubjectsByTeacherIdAsync(teacherId);
        }

        public async Task<IEnumerable<Subject>> GetAvailableSubjectsByStudentIdAsync(int studentId)
        {
            return await _subjectRepository.GetAvailableSubjectsByStudentIdAsync(studentId);
        }

        public async Task CreateSubjectAsync(Subject subject)
        {
            await _subjectRepository.CreateSubjectAsync(subject);
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            await _subjectRepository.UpdateSubjectAsync(subject);
        }

        public async Task DeleteSubjectAsync(int id)
        {
            await _subjectRepository.DeleteSubjectAsync(id);
        }

        public async Task<bool> SubjectExistsAsync(string name, string code)
        {
            return await _subjectRepository.SubjectExistsAsync(name, code);
        }
    }
}
