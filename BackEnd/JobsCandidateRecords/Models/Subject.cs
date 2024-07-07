using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Subject")]
    [Index(nameof(Name), IsUnique = true)]
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
