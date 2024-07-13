using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("ApplicaionsForRequests")]
    public class ApplicationsForRequests
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        [SwaggerIgnore]
        public virtual Application? Application { get; set; }

        public int RequestForEmployeeId { get; set; }
        [SwaggerIgnore]
        public virtual RequestForEmployee? RequestForEmployee { get; set; }
    }
}
