using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("ApplicationStatusHistory")]
    public class ApplicationStatusHistory
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public Application? Application { get; set; }

        [MaxLength(100)]
        public string UpdatedApplicationStatusName { get; set; } = string.Empty;

        //public int ApplicationStatusId { get; set; }
        //public ApplicationStatus? ApplicationStatus { get; set; }


        public DateTime? StatusTime { get; set; } = null;
    }
}
