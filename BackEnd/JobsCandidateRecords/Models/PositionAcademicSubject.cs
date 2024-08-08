using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents the relationship between a position and an academic subject.
    /// </summary>
    [Table("PositionAcademicSubjects")]
    public class PositionAcademicSubject
    {
        /// <summary>
        /// Gets or sets the unique identifier for the PositionAcademicSubject.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the related position.
        /// </summary>
        [Required]
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the related position.
        /// </summary>
        [SwaggerIgnore]
        public virtual Position? Position { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the related academic subject.
        /// </summary>
        [Required]
        public int AcademicSubjectId { get; set; }

        /// <summary>
        /// Gets or sets the related academic subject.
        /// </summary>
        [SwaggerIgnore]
        public virtual AcademicSubject? AcademicSubject { get; set; }
    }
}
