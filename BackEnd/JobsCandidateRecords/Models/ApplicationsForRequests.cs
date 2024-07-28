using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents the link between applications and requests for employees.
    /// </summary>
    [Table("ApplicationsForRequests")]
    public class ApplicationsForRequests
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application associated with this link.
        /// </summary>
        [SwaggerIgnore]
        public virtual Application? Application { get; set; }

        /// <summary>
        /// Gets or sets the request for employee identifier.
        /// </summary>
        public int RequestForEmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the request for employee associated with this link.
        /// </summary>
        [SwaggerIgnore]
        public virtual RequestForEmployee? RequestForEmployee { get; set; }
    }
}
