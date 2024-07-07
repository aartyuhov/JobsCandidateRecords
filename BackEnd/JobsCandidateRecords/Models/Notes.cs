using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Notes")]
    public class Notes
    {
        public int Id { get; set; }

        public int? CandidateId { get; set; }
        public Candidate? Candidate { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [MaxLength(255)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
