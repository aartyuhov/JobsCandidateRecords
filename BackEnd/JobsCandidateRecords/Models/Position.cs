using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents a job position within the company.
    /// </summary>
    [Table("Positions")]
    [Index(nameof(Title), IsUnique = true)]
    public class Position
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Position.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the Position.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the responsibilities associated with the Position.
        /// </summary>
        [MaxLength(255)]
        public string Responsibilities { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the related department.
        /// </summary>
        [Required]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the related department.
        /// </summary>
        [SwaggerIgnore]
        public virtual Department? Department { get; set; }

        /// <summary>
        /// Gets or sets the employees holding this position.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }

        /// <summary>
        /// Gets or sets the requests for employees for this position.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<RequestForEmployee>? RequestForEmployees { get; set; }

        /// <summary>
        /// Gets or sets the academic subjects related to this position.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<PositionAcademicSubject>? PositionAcademicSubjects { get; set; }
    }
}
