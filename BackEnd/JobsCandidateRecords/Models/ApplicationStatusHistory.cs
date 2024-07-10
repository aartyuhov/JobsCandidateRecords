using JobsCandidateRecords.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("ApplicationStatusHistory")]
    public class ApplicationStatusHistory
    {
        public int Id { get; set; }

        [Required]
        public int ApplicationId { get; set; }
        public virtual Application? Application { get; set; }

        [Required]
        public ApplicationStatusEnum ApplicationStatus { get; set; }

        public DateTime? DecisionDate { get; set; } = null;
    }
}
