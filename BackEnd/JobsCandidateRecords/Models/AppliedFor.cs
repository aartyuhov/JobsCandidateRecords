using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("AppliedFor")]
    public class AppliedFor
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public Application? Application { get; set; }

        [MaxLength(30)]
        public string? JobCode { get; set; } = null;
        //public int JobId { get; set; }
        //public Job? Job { get; set; }

        public bool isSelected { get; set; } = false;
    }
}
