using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("AcademicSubjects")]
    [Index(nameof(Name), IsUnique = true)]
    public class AcademicSubject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;
        [SwaggerIgnore]
        public virtual ICollection<PositionAcademicSubject>? PositionAcademicSubject { get; set; }
    }
}
