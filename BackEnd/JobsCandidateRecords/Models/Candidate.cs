using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents a candidate.
    /// </summary>
    [Table("Candidates")]
    [Index(nameof(Email), IsUnique = true)]
    public class Candidate
    {
        /// <summary>
        /// Gets or sets the unique identifier for the candidate.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the candidate.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the candidate.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth of the candidate.
        /// </summary>
        public DateOnly? DateOfBirth { get; set; } = null;

        /// <summary>
        /// Gets or sets the gender of the candidate.
        /// </summary>
        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the candidate.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the candidate.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the candidate.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the additional information about the candidate.
        /// </summary>
        [MaxLength(255)]
        public string AboutInfo { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the applications associated with the candidate.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Application>? Applications { get; set; }
    }
}
