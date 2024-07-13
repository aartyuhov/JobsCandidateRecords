using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("RequestForEmployees")]
    public class RequestForEmployee
    {
        public int Id { get; set; }

        
        public string Name { get; set; } = string.Empty;

        public DateTime? PublicationDate { get; set; } = null;

        public int NumberEmployessRequired { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        public int? PositionId { get; set; }
        [SwaggerIgnore]
        public virtual Position? Position { get; set; }

        public int RequestedEmployeeId { get; set; }
        [SwaggerIgnore]
        public virtual Employee? RequestedEmployee { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<ApplicationsForRequests>? ApplicaionsForRequests { get; set; }
    }
}
