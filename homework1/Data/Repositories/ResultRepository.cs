using homework1.Data.Interfaces;
using homework1.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace homework1.Data.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly SchoolDbContext _context;

        public ResultRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Result>, int)> GetResultsIndexAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string searchTerm = null)
        {
            var pageNumberParam = new SqlParameter("@PageNumber", SqlDbType.Int) { Value = pageNumber };
            var pageSizeParam = new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize };
            var searchTermParam = new SqlParameter("@SearchTerm", SqlDbType.NVarChar, 30) { Value = searchTerm ?? (object)DBNull.Value };
            var totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var result = await _context.Results
                .FromSqlRaw("EXEC GetResultsIndex @PageNumber, @PageSize, @SearchTerm, @TotalCount OUTPUT",
                pageNumberParam, pageSizeParam, searchTermParam, totalCountParam)
                .ToListAsync();

            var totalCount = totalCountParam.Value != DBNull.Value ? (int)totalCountParam.Value : 0;

            return (result, totalCount);
        }

        public async Task<IEnumerable<Result>> GetResultsAsync()
        {
            var results = await _context.Results
                .FromSqlRaw("EXEC GetAllResults")
                .ToListAsync();

            return results;
        }

        public async Task<IEnumerable<Result>> GetResultByIdAsync(int id)
        {
            var param = new SqlParameter("@ResultId", id);

            var result = await _context.Results
                .FromSqlRaw("EXEC GetResultById @ResultId", param)
                .ToListAsync();

            return result ?? throw new InvalidOperationException("Result not found");
        }

        public async Task<IEnumerable<Result>> GetResultByStudentIdAsync(int studentId)
        {
            var param = new SqlParameter("@StudentId", studentId);

            var results = await _context.Results
                .FromSqlRaw("EXEC GetResultByStudentId @StudentId", param)
                .ToListAsync();

            return results;
        }
        public async Task<IEnumerable<Subject>> GetAvailableSubjectsByStudentId(int studentId)
        {
            var subjects = await _context.Subjects
                .FromSqlRaw("EXEC GetAvailableSubjectsByStudentId @StudentId", new SqlParameter("@StudentId", studentId))
                .ToListAsync();

            return subjects;
        }

        public async Task CreateResultAsync(Result result)
        {
            // Parameters for each condition
            var notPassedSubjectCountParam = new SqlParameter("@NotPassedSubjectCount", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
            var resultExistsParam = new SqlParameter("@ResultExists", System.Data.SqlDbType.Bit) { Direction = System.Data.ParameterDirection.Output };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CheckResultConditions @StudentId, @SubjectId, @NotPassedSubjectCount OUTPUT, @ResultExists OUTPUT",
                new SqlParameter("@StudentId", result.StudentId),
                new SqlParameter("@SubjectId", result.SubjectId),
                notPassedSubjectCountParam, resultExistsParam
            );

            int notPassedSubjectCount = (int)notPassedSubjectCountParam.Value;
            bool resultExists = (bool)resultExistsParam.Value;

            // Checking conditions
            if (notPassedSubjectCount >= 10)
            {
                throw new InvalidOperationException("Student has already taken the maximum (10) allowed subjects that have not been passed.");
            }

            if (resultExists)
            {
                throw new InvalidOperationException("Student has already taken this subject.");
            }

            var param = new List<SqlParameter>
            {
                new SqlParameter("@Marks", SqlDbType.Decimal) { Precision = 4, Scale = 1, Value = result.Marks != null ? (decimal)result.Marks : DBNull.Value },
                new SqlParameter("@StudentId", SqlDbType.Int) { Value = result.StudentId },
                new SqlParameter("@SubjectId", SqlDbType.Int) { Value = result.SubjectId }
            };

            // Execute Result creation
            await _context.Database.ExecuteSqlRawAsync("EXEC CreateResult @Marks, @StudentId, @SubjectId", param);

        }

        public async Task UpdateResultAsync(Result result)
        {
            // Parameters for each condition
            var notPassedSubjectCountParam = new SqlParameter("@NotPassedSubjectCount", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
            var resultExistsParam = new SqlParameter("@ResultExists", System.Data.SqlDbType.Bit) { Direction = System.Data.ParameterDirection.Output };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CheckResultConditions @StudentId, @SubjectId, @NotPassedSubjectCount OUTPUT, @ResultExists OUTPUT",
                new SqlParameter("@StudentId", result.StudentId),
                new SqlParameter("@SubjectId", result.SubjectId),
                notPassedSubjectCountParam, resultExistsParam
            );

            int notPassedSubjectCount = (int)notPassedSubjectCountParam.Value;

            // Checking conditions
            if (notPassedSubjectCount >= 10 && (result.Marks < 50 || result.Marks == null))
            {
                throw new InvalidOperationException("Student has already taken the maximum (10) allowed subjects that have not been passed.");
            }

            else
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter("@ResultId", result.ResultId),
                    new SqlParameter("@Marks", SqlDbType.Decimal) { Precision = 4, Scale = 1, Value = result.Marks != null ? (decimal)result.Marks : DBNull.Value }
                };

                // Execute Result update
                await _context.Database.ExecuteSqlRawAsync("EXEC UpdateResult @ResultId, @Marks", param);
            }
        }

        public async Task DeleteResultAsync(int id)
        {
            var param = new SqlParameter("@ResultId", id);

            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteResult @ResultId", param);
        }
    }
}