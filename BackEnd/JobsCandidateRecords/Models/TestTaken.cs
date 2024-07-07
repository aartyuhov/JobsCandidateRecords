using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("TestTaken")]
    public class TestTaken
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public Application? Application { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? Start { get; set; } = null;

        public DateTime? End { get; set; } = null;

        public int TestStatusId { get; set; }
        public TestStatus? TestStatus { get; set; }

        [MaxLength(255)]
        public string Notes { get; set; } = string.Empty;

        public int? TotalScore { get; set; } = null;

        public int MaxScore { get; set; } = 100;

        public List<EmployeeGrade>? EmployeeGrades { get; set; }

    }
}
