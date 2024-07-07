using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Job")]
    [Index(nameof(Name), IsUnique = true)]
    public class Job
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string PostionTitle { get; set; } = string.Empty;

        public DateTime? PostedDate { get; set; } = null;

        public DateTime? StartDate { get; set; } = null;

        public int Employess_needed { get; set; } = 0;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public Company? Company { get; set; }


        public List<PostedOn>? PostedOns { get; set; }

        public List<Application>? Applications { get; set; }
    }
}
