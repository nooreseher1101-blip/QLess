using Dapper;
using QLessProject.Data;
using QLessProject.Models;
using QLessProject.Repositories.Interfaces;

namespace QLessProject.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DapperContext _context;

        public DepartmentRepository(DapperContext context)
        {
            _context = context;
        }

        // Get all departments
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var query = "SELECT * FROM Departments";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Department>(query);
            }
        }

        // Get department by ID
        public async Task<Department?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Departments WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Department>(
                    query, new { Id = id });
            }
        }

        // Add department
        public async Task<int> AddAsync(Department department)
        {
            var query = @"
                INSERT INTO Departments (Name, Description, IsActive)
                VALUES (@Name, @Description, @IsActive);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(
                    query, department);
            }
        }

        // Update department
        public async Task<bool> UpdateAsync(Department department)
        {
            var query = @"
                UPDATE Departments
                SET Name = @Name,
                    Description = @Description,
                    IsActive = @IsActive
                WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rows = await connection.ExecuteAsync(query, department);
                return rows > 0;
            }
        }

        // Delete department
        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM Departments WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rows = await connection.ExecuteAsync(
                    query, new { Id = id });

                return rows > 0;
            }
        }
    }
}