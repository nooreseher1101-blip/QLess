using Dapper;
using QLessProject.Data;
using QLessProject.Models;
using QLessProject.Repositories.Interfaces;

namespace QLessProject.Repositories
{
    public class CounterRepository : ICounterRepository
    {
        private readonly DapperContext _context;

        public CounterRepository(DapperContext context)
        {
            _context = context;
        }

        // Get all counters
        public async Task<IEnumerable<Counter>> GetAllAsync()
        {
            var query = @"
                SELECT c.*, d.Name AS DepartmentName
                FROM Counters c
                INNER JOIN Departments d ON c.DepartmentId = d.Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Counter>(query);
            }
        }

        // Get counters by department
        public async Task<IEnumerable<Counter>> GetByDepartmentIdAsync(int departmentId)
        {
            var query = @"
                SELECT *
                FROM Counters
                WHERE DepartmentId = @DepartmentId";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Counter>(
                    query, new { DepartmentId = departmentId });
            }
        }

        // Get counter by ID
        public async Task<Counter?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Counters WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Counter>(
                    query, new { Id = id });
            }
        }

        // Add counter
        public async Task<int> AddAsync(Counter counter)
        {
            var query = @"
                INSERT INTO Counters (Name, DepartmentId, IsActive)
                VALUES (@Name, @DepartmentId, @IsActive);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(
                    query, counter);
            }
        }

        // Update counter
        public async Task<bool> UpdateAsync(Counter counter)
        {
            var query = @"
                UPDATE Counters
                SET Name = @Name,
                    DepartmentId = @DepartmentId,
                    IsActive = @IsActive
                WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rows = await connection.ExecuteAsync(query, counter);
                return rows > 0;
            }
        }

        // Delete counter
        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM Counters WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rows = await connection.ExecuteAsync(
                    query, new { Id = id });

                return rows > 0;
            }
        }
    }
}