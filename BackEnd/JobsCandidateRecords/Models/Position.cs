using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Position")]
    [Index(nameof(Title), IsUnique = true)]
    public class Position
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]

        public string Title { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Responsibilities { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Job>? Jobs { get; set; }

    }
}
