using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Application")]
    public class Application
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int ApplicationStatusId { get; set; }
        public ApplicationStatus? ApplicationStatus { get; set; }

        public DateTime? Created { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string Details { get; set; } = string.Empty;

        public List<AppliedFor>? AppliedFors { get; set; }

        public List<ApplicationStatusHistory>? ApplicationStatusHistories { get; set; }

        public List<TestTaken>? TestTakens { get; set; }


    }
}
