using QLessProject.Models;

namespace QLessProject.Repositories.Interfaces
{
    public interface IQueueTicketRepository
    {
        Task<IEnumerable<QueueTicket>> GetAllAsync();

        Task<IEnumerable<QueueTicket>> GetByUserIdAsync(string userId);

        Task<IEnumerable<QueueTicket>> GetByDepartmentIdAsync(int departmentId);

        Task<QueueTicket?> GetByIdAsync(int id);

        Task<int> AddAsync(QueueTicket ticket);

        Task<bool> UpdateStatusAsync(int id, string status);

        Task<bool> DeleteAsync(int id);
    }
}