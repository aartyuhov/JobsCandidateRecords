using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("ApplicationStatus")]
    [Index(nameof(Name), IsUnique = true)]
    public class ApplicationStatus
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<Application>? Applications { get; set; }
        public List<ApplicationStatusHistory>? ApplicationStatusHistory { get; set; }

    }
}
