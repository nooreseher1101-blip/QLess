using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLessProject.Models
{
    public class QueueTicket
    {
        public int Id { get; set; }

        [Required]
        public string TicketNumber { get; set; }

        public string? UserId { get; set; }

        public int DepartmentId { get; set; }

        public int CounterId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [ForeignKey("CounterId")]
        public Counter? Counter { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Waiting";
    }
}