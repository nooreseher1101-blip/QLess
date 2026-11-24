using Dapper;
using QLessProject.Data;
using QLessProject.Models;
using QLessProject.Repositories.Interfaces;

namespace QLessProject.Repositories
{
    public class QueueTicketRepository : IQueueTicketRepository
    {
        private readonly DapperContext _context;

        public QueueTicketRepository(DapperContext context)
        {
            _context = context;
        }

        // Get all tickets
        public async Task<IEnumerable<QueueTicket>> GetAllAsync()
        {
            var query = "SELECT * FROM QueueTickets ORDER BY CreatedAt DESC";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<QueueTicket>(query);
            }
        }

        // Get tickets of a specific user
        public async Task<IEnumerable<QueueTicket>> GetByUserIdAsync(string userId)
        {
            var query = @"
                SELECT *
                FROM QueueTickets
                WHERE UserId = @UserId
                ORDER BY CreatedAt DESC";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<QueueTicket>(
                    query, new { UserId = userId });
            }
        }

        // Get tickets of a specific department
        public async Task<IEnumerable<QueueTicket>> GetByDepartmentIdAsync(int departmentId)
        {
            var query = @"
                SELECT *
                FROM QueueTickets
                WHERE DepartmentId = @DepartmentId
                ORDER BY CreatedAt ASC";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<QueueTicket>(
                    query, new { DepartmentId = departmentId });
            }
        }

        // Get ticket by ID
        public async Task<QueueTicket?> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM QueueTickets WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<QueueTicket>(
                    query, new { Id = id });
            }
        }

        // Add new ticket
        public async Task<int> AddAsync(QueueTicket ticket)
        {
            var query = @"
                INSERT INTO QueueTickets
                (TicketNumber, UserId, DepartmentId, CounterId, CreatedAt, Status)
                VALUES
                (@TicketNumber, @UserId, @DepartmentId, @CounterId, @CreatedAt, @Status);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(
                    query, ticket);
            }
        }

        // Update ticket status
        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var query = @"
                UPDATE QueueTickets
                SET Status = @Status
                WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rows = await connection.ExecuteAsync(
                    query, new { Id = id, Status = status });

                return rows > 0;
            }
        }

        // Delete ticket
        public async Task<bool> DeleteAsync(int id)
        {
            var query = "DELETE FROM QueueTickets WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var rows = await connection.ExecuteAsync(
                    query, new { Id = id });

                return rows > 0;
            }
        }
    }
}