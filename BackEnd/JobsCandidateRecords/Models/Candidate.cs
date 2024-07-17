using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Candidates")]
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

        public DateOnly? DateOfBirth { get; set; } = null;

        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [MaxLength(255)]
        public string AboutInfo { get; set; } = string.Empty;
        [SwaggerIgnore]
        public virtual ICollection<Application>? Applications { get; set; }
    }
}
