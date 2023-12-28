using homework1.Models;

namespace homework1.Data.Interfaces
{
    public interface IResultService
    {
        Task<(IEnumerable<Result>, int)> GetResultsIndexAsync(int pageNumber, int pageSize, string searchTerm);
        Task<IEnumerable<Result>> GetResultsAsync();
        Task<IEnumerable<Result>> GetResultByIdAsync(int id);
        Task<IEnumerable<Result>> GetResultByStudentIdAsync(int studentId);
        Task<IEnumerable<Subject>> GetAvailableSubjectsByStudentId(int studentId);
        Task CreateResultAsync(Result result);
        Task UpdateResultAsync(Result result);
        Task DeleteResultAsync(int id);
    }
}
