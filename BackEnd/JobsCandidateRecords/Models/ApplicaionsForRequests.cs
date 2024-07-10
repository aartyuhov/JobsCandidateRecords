using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("ApplicaionsForRequests")]
    public class ApplicaionsForRequests
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public virtual Application? Application { get; set; }

        public int RequestForEmployeeId { get; set; }
        public virtual RequestForEmployee? RequestForEmployee { get; set; }
    }
}
