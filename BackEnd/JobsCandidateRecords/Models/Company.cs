using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{

    [Table("Company")]
    [Index(nameof(Name), IsUnique = true)]
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public List<Job>? Jobs { get; set; }
    }
}
