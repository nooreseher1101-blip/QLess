using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLessProject.Models
{
    public class StaffProfile
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int DepartmentId { get; set; }

        public int? CounterId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [ForeignKey("CounterId")]
        public Counter? Counter { get; set; }
    }
}