using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents an academic subject.
    /// </summary>
    [Table("AcademicSubjects")]
    [Index(nameof(Name), IsUnique = true)]
    public class AcademicSubject
    {
        /// <summary>
        /// Gets or sets the unique identifier for the academic subject.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the academic subject.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the academic subject.
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of position academic subjects associated with this academic subject.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<PositionAcademicSubject>? PositionAcademicSubjects { get; set; }
    }
}
