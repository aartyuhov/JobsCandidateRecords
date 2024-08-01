using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents a request for employees.
    /// </summary>
    [Table("RequestForEmployees")]
    public class RequestForEmployee
    {
        /// <summary>
        /// Request identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Request name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Date of publication of the request.
        /// </summary>
        public DateTime? PublicationDate { get; set; } = null;

        /// <summary>
        /// Number of employees required.
        /// </summary>
        public int NumberEmployessRequired { get; set; } = 0;

        /// <summary>
        /// Request description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Position identifier.
        /// </summary>
        public int? PositionId { get; set; }

        /// <summary>
        /// Position associated with the request.
        /// </summary>
        [SwaggerIgnore]
        public virtual Position? Position { get; set; }

        /// <summary>
        /// Requested employee identifier.
        /// </summary>
        public int RequestedEmployeeId { get; set; }

        /// <summary>
        /// Requested employee associated with the request.
        /// </summary>
        [SwaggerIgnore]
        public virtual Employee? RequestedEmployee { get; set; }

        /// <summary>
        /// Collection of applications for the request.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<ApplicationsForRequests>? ApplicationsForRequests { get; set; }
    }
}
