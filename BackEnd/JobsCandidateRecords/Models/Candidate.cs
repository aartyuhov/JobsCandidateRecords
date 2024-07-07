using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Candidate")]
    [Index(nameof(Email), IsUnique = true)]
    public class Candidate
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100")]
        public int? Age { get; set; } = null;

        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Summary { get; set; } = string.Empty;

        public List<Notes>? Notes { get; set; }

        public List<RelatedDocument>? RelatedDocuments { get; set; }

    }
}
