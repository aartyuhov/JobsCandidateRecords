using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Job")]
    [Index(nameof(Code), IsUnique = true)]
    public class Job
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Code { get; set; } = null;

        public DateTime? PostedDate { get; set; } = null;

        public DateTime? StartDate { get; set; } = null;

        public int Employess_needed { get; set; } = 0;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public List<PostedOn>? PostedOns { get; set; }

        public List<AppliedFor>? AppliedFors { get; set; }
    }
}
