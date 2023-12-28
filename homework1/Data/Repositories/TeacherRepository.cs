using homework1.Data.Interfaces;
using homework1.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace homework1.Data.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Teacher>, int)> GetTeachersIndexAsync(
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

                var result = await _context.Teachers
                    .FromSqlRaw("EXEC GetTeachersIndex @PageNumber, @PageSize, @SearchTerm, @TotalCount OUTPUT",
                    pageNumberParam, pageSizeParam, searchTermParam, totalCountParam)
                    .ToListAsync();

                var totalCount = totalCountParam.Value != DBNull.Value ? (int)totalCountParam.Value : 0;

                return (result, totalCount);
            }
        }

        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            var teachers = await _context.Teachers
                .FromSqlRaw("EXEC GetAllTeachers")
                .ToListAsync();

            return teachers;
        }

        public async Task<IEnumerable<Teacher>> GetTeacherByIdAsync(int id)
        {
            var param = new SqlParameter("@TeacherId", id);

            var result = await _context.Teachers
                .FromSqlRaw("EXEC GetTeacherById @TeacherId", param)
                .ToListAsync();

            return result ?? throw new InvalidOperationException("Teacher not found");
        }

        public async Task CreateTeacherAsync(Teacher teacher)
        {
            if (await TeacherExistsAsync(teacher.Email, teacher.PhoneNumber))
            {
                throw new InvalidOperationException("Teacher with the same email or phone number already exists.");
            }

            var param = new List<SqlParameter>
            {
                new SqlParameter("@Name", teacher.Name),
                new SqlParameter("@Email", teacher.Email),
                new SqlParameter("@PhoneNumber", teacher.PhoneNumber)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC CreateTeacher @Name, @Email, @PhoneNumber", param.ToArray());
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter("@TeacherId", teacher.TeacherId),
                new SqlParameter("@Name", teacher.Name),
                new SqlParameter("@Email", teacher.Email),
                new SqlParameter("@PhoneNumber", teacher.PhoneNumber)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateTeacher @TeacherId, @Name, @Email, @PhoneNumber", param.ToArray());
        }

        public async Task DeleteTeacherAsync(int teacherId)
        {
            var param = new SqlParameter("@TeacherId", teacherId);

            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteTeacher @TeacherId", param);
        }

        public async Task<bool> TeacherExistsAsync(string email, string phoneNumber)
        {
            var emailParam = new SqlParameter("@Email", email);
            var phoneNumberParam = new SqlParameter("@PhoneNumber", phoneNumber);

            var teacherExistsParam = new SqlParameter
            {
                ParameterName = "@TeacherExists",
                SqlDbType = System.Data.SqlDbType.Bit,
                Direction = System.Data.ParameterDirection.Output
            };

            var result = await _context.Database
                .ExecuteSqlRawAsync("EXEC CheckTeacherExists @Email, @PhoneNumber, @TeacherExists OUTPUT",
                    emailParam, phoneNumberParam, teacherExistsParam);

            return (bool)teacherExistsParam.Value;
        }
    }
}