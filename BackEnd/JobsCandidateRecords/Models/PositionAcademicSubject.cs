using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("PositionAcademicSubject")]
    public class PositionAcademicSubject
    {
        public int Id { get; set; }

        [Required]
        public int PositionId { get; set; }
        [SwaggerIgnore]
        public virtual Position? Position { get; set; }

        [Required]
        public int AcademicSubjectId { get; set; }
        [SwaggerIgnore]
        public virtual AcademicSubject? AcademicSubject { get; set; }
    }
}
