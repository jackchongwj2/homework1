using homework1.Data.Interfaces;
using homework1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using NuGet.DependencyResolver;
using System.Data;

namespace homework1.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Student>, int)> GetStudentsIndexAsync(
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

                var result = await _context.Students
                    .FromSqlRaw("EXEC GetStudentsIndex @PageNumber, @PageSize, @SearchTerm, @TotalCount OUTPUT",
                    pageNumberParam, pageSizeParam, searchTermParam, totalCountParam)
                    .ToListAsync();

                var totalCount = totalCountParam.Value != DBNull.Value ? (int)totalCountParam.Value : 0;

                return (result, totalCount);
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students = await _context.Students
                .FromSqlRaw("EXEC GetAllStudents")
                .ToListAsync();

            return students;
        }

        public async Task<IEnumerable<Student>> GetStudentByIdAsync(int id)
        {
            var param = new SqlParameter("@StudentId", id);

            var result = await _context.Students
                .FromSqlRaw("EXEC GetStudentById @StudentId", param)
                .ToListAsync();

            return result ?? throw new InvalidOperationException("Student not found");
        }

        public async Task CreateStudentAsync(Student student)
        {
            if (await StudentExistsAsync(student.Email))
            {
                throw new InvalidOperationException("Teacher with the same email or phone number already exists.");
            }

            var param = new List<SqlParameter>
            {
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@Email", student.Email),
                new SqlParameter("@DOB", student.DOB),
                new SqlParameter("@EnrollmentDate", student.EnrollmentDate)
            };

            var parameterNames = string.Join(", ", param.Select(p => p.ParameterName));

            var sqlCommand = $"EXEC CreateStudent {parameterNames}";

            await _context.Database.ExecuteSqlRawAsync(sqlCommand, param.ToArray());
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@StudentId", student.StudentId),
                new SqlParameter("@Name", student.Name),
                new SqlParameter("@Email", student.Email),
                new SqlParameter("@DOB", student.DOB),
                new SqlParameter("@EnrollmentDate", student.EnrollmentDate)
            };

            var parameterNames = string.Join(", ", param.Select(p => p.ParameterName));

            var sqlCommand = $"EXEC UpdateStudent {parameterNames}";

            await _context.Database.ExecuteSqlRawAsync(sqlCommand, param.ToArray());
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var param = new SqlParameter("@StudentId", studentId);

            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteStudent @StudentId", param);
        }

        public async Task<bool> StudentExistsAsync(string email)
        {
            var emailParam = new SqlParameter("@Email", email);

            var studentExistsParam = new SqlParameter
            {
                ParameterName = "@StudentExists",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };

            var result = await _context.Database
                .ExecuteSqlRawAsync("EXEC CheckStudentExists @Email, @StudentExists OUTPUT",
                    emailParam, studentExistsParam);

            return (bool)studentExistsParam.Value;
        }
    }
}
