using homework1.Data.Interfaces;
using homework1.Data.Repositories;
using homework1.Models;

namespace homework1.Data.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<(IEnumerable<Result>, int)> GetResultsIndexAsync(
            int pageNumber, int pageSize, string searchTerm)
        {
            return await _resultRepository.GetResultsIndexAsync(pageNumber, pageSize, searchTerm);
        }

        public async Task<IEnumerable<Result>> GetResultsAsync()
        {
            return await _resultRepository.GetResultsAsync();
        }

        public async Task<IEnumerable<Result>> GetResultByIdAsync(int id)
        {
            return await _resultRepository.GetResultByIdAsync(id);
        }

        public async Task<IEnumerable<Result>> GetResultByStudentIdAsync(int studentId)
        {
            return await _resultRepository.GetResultByStudentIdAsync(studentId);
        }
        public async Task<IEnumerable<Subject>> GetAvailableSubjectsByStudentId(int studentId)
        {
            // Call the repository method to get available subjects
            return await _resultRepository.GetAvailableSubjectsByStudentId(studentId);
        }

        public async Task CreateResultAsync(Result result)
        {
            await _resultRepository.CreateResultAsync(result);
        }

        public async Task UpdateResultAsync(Result result)
        {
            await _resultRepository.UpdateResultAsync(result);
        }

        public async Task DeleteResultAsync(int id)
        {
            await _resultRepository.DeleteResultAsync(id);
        }

    }
}
