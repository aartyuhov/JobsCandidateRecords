using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents a company entity.
    /// </summary>
    [Table("Companies")]
    [Index(nameof(Name), IsUnique = true)]
    public class Company
    {
        /// <summary>
        /// Gets or sets the company ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the company.
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of departments within the company.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Department>? Departments { get; set; }
    }
}
