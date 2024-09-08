using JobsCandidateRecords.Enums;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents the history of application statuses.
    /// </summary>
    [Table("ApplicationStatusHistory")]
    public class ApplicationStatusHistory
    {
        /// <summary>
        /// Gets or sets the unique identifier for the application status history.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the related application.
        /// </summary>
        [Required]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the identity user associated with the creating application status hystory.
        /// </summary>
        public string? IdentityUserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee associated with the creating application status hystory.
        /// </summary>
        public string? EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the related application.
        /// </summary>
        [SwaggerIgnore]
        public virtual Application? Application { get; set; }

        /// <summary>
        /// Gets or sets the status of the application.
        /// </summary>
        [Required]
        public ApplicationStatusEnum ApplicationStatus { get; set; }

        /// <summary>
        /// Gets or sets the decision date of the application status.
        /// </summary>
        public DateTime? DecisionDate { get; set; } = null;
    }
}
