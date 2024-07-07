using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("EmployeeGrade")]
    public class EmployeeGrade
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int? TestTakenId { get; set; } = null;
        //public TestTaken? TestTaken { get; set; }

        public int? Score { get; set; } = null;

        [MaxLength(255)]
        public string Notes { get; set; } = string.Empty;
    }
}
