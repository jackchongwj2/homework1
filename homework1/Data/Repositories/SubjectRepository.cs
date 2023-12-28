using homework1.Data.Interfaces; 
using homework1.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Data;

namespace homework1.Data.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolDbContext _context;

        public SubjectRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Subject>, int)> GetSubjectsIndexAsync(
            int pageNumber = 1,
            int pageSize = 10,
            string searchTerm = null)
        {
            using (_context)
            {
                var pageNumberParam = new SqlParameter("@PageNumber", SqlDbType.Int) { Value = pageNumber };
                var pageSizeParam = new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize };
                var searchTermParam = new SqlParameter("@SearchTerm", SqlDbType.NVarChar, 30) { Value = searchTerm ?? (object)DBNull.Value };
                var totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int) { Direction = ParameterDirection.Output };

                var result = await _context.Subjects
                    .FromSqlRaw("EXEC GetSubjectsIndex @PageNumber, @PageSize, @SearchTerm, @TotalCount OUTPUT",
                    pageNumberParam, pageSizeParam, searchTermParam, totalCountParam)
                    .ToListAsync();

                var totalCount = totalCountParam.Value != DBNull.Value ? (int)totalCountParam.Value : 0;

                return (result, totalCount);
            }
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            var subjects = await _context.Subjects
                .FromSqlRaw("EXEC GetAllSubjects")
                .ToListAsync();

            return subjects;
        }

        public async Task<IEnumerable<Subject>> GetSubjectByIdAsync(int id)
        {
            var param = new SqlParameter("@SubjectId", id);

            var result = await _context.Subjects
                .FromSqlRaw("EXEC GetSubjectById @SubjectId", param)
                .ToListAsync();

            return result ?? throw new InvalidOperationException("Subject not found");
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            var param = new SqlParameter("@TeacherId", teacherId);

            return await _context.Subjects
                .FromSqlRaw("EXEC GetSubjectsByTeacherId @TeacherId", param)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetAvailableSubjectsByStudentIdAsync(int studentId)
        {
            var param = new SqlParameter("@StudentId", studentId);

            return await _context.Subjects
                .FromSqlRaw("EXEC GetAvailableSubjectsByStudentId @StudentId", param)
                .ToListAsync();
        }

        public async Task CreateSubjectAsync(Subject subject)
        {
            if (await SubjectExistsAsync(subject.Name, subject.Code))
            {
                throw new InvalidOperationException("Subject with the same name or code already exists.");
            }

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", subject.Name),
                new SqlParameter("@Code", subject.Code),
                new SqlParameter("@TeacherId", subject.TeacherId)
            };

            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));

            var sqlCommand = $"EXEC CreateSubject {parameterNames}";

            await _context.Database.ExecuteSqlRawAsync(sqlCommand, parameters.ToArray());
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@SubjectId", subject.SubjectId),
                new SqlParameter("@Name", subject.Name),
                new SqlParameter("@Code", subject.Code),
                new SqlParameter("@TeacherId", subject.TeacherId)
            };

            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));

            var sqlCommand = $"EXEC UpdateSubject {parameterNames}";

            await _context.Database.ExecuteSqlRawAsync(sqlCommand, parameters.ToArray());
        }

        public async Task DeleteSubjectAsync(int subjectId)
        {
            var param = new SqlParameter("@SubjectId", subjectId);

            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteSubject @SubjectId", param);
        }

        public async Task<bool> SubjectExistsAsync(string name, string code)
        {
            var nameParam = new SqlParameter("@Name", name);
            var codeParam = new SqlParameter("@Code", code);

            var subjectExistsParam = new SqlParameter
            {
                ParameterName = "@SubjectExists",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };

            var result = await _context.Database
                .ExecuteSqlRawAsync("EXEC CheckSubjectExists @Name, @Code, @SubjectExists OUTPUT",
                    nameParam, codeParam, subjectExistsParam);

            return (bool)subjectExistsParam.Value;
        }
    }
}