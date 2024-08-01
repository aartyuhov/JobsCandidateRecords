using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents a department within a company.
    /// </summary>
    [Table("Departments")]
    [Index(nameof(Name), IsUnique = true)]
    public class Department
    {
        /// <summary>
        /// Gets or sets the department ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the department.
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company ID to which the department belongs.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company to which the department belongs.
        /// </summary>
        [SwaggerIgnore]
        public virtual Company? Company { get; set; }

        /// <summary>
        /// Gets or sets the positions within the department.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Position>? Positions { get; set; }
    }
}
