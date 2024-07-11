using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Department")]
    [Index(nameof(Name), IsUnique = true)]
    public class Department
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public virtual Company? Company { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<Position>? Positions { get; set; }
    }
}
