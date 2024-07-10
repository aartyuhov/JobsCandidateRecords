using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("RequestForEmployee")]
    public class RequestForEmployee
    {
        public int Id { get; set; }

        
        public string Name { get; set; } = string.Empty;

        public DateTime? PublicationDate { get; set; } = null;

        public int NumberEmployessRequired { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public int? PositionId { get; set; }
        public virtual Position? Position { get; set; }

        public int RequestedEmployeeId { get; set; }
        public virtual Employee? RequestedEmployee { get; set; }

        public virtual ICollection<ApplicaionsForRequests>? ApplicaionsForRequests { get; set; }
    }
}
