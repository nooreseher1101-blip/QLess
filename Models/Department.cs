using System.ComponentModel.DataAnnotations;

namespace QLessProject.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(250)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}