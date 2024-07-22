using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Applications")]
    public class Application
    {
        public int Id { get; set; }

        public int CandidateId { get; set; }
        [SwaggerIgnore]
        public virtual Candidate? Candidate { get; set; }

        public int? EmployeeWhoCreatedId { get; set; }
        [SwaggerIgnore]
        public virtual Employee? EmployeeWhoCreated { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.Now;

        [MaxLength(255)]
        public string Details { get; set; } = string.Empty;
        [SwaggerIgnore]
        public virtual ICollection<ApplicationsForRequests>? ApplicationsForRequests { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<ApplicationStatusHistory>? ApplicationStatusHistories { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<Note>? Notes { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<Attachment>? Attachments { get; set; }
    }
}