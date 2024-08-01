using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents an application.
    /// </summary>
    [Table("Applications")]
    public class Application
    {
        /// <summary>
        /// Gets or sets the unique identifier for the application.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the candidate identifier associated with the application.
        /// </summary>
        public int CandidateId { get; set; }

        /// <summary>
        /// Gets or sets the candidate associated with the application.
        /// </summary>
        [SwaggerIgnore]
        public virtual Candidate? Candidate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the employee who created the application.
        /// </summary>
        public int? EmployeeWhoCreatedId { get; set; }

        /// <summary>
        /// Gets or sets the employee who created the application.
        /// </summary>
        [SwaggerIgnore]
        public virtual Employee? EmployeeWhoCreated { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the application.
        /// </summary>
        public DateTime? CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the details of the application.
        /// </summary>
        [MaxLength(255)]
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of application requests associated with the application.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<ApplicationsForRequests>? ApplicationsForRequests { get; set; }

        /// <summary>
        /// Gets or sets the collection of application status histories associated with the application.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<ApplicationStatusHistory>? ApplicationStatusHistories { get; set; }

        /// <summary>
        /// Gets or sets the collection of notes associated with the application.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Note>? Notes { get; set; }

        /// <summary>
        /// Gets or sets the collection of attachments associated with the application.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Attachment>? Attachments { get; set; }
    }
}