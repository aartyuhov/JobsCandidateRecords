using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents an attachment associated with an application.
    /// </summary>
    [Table("Attachments")]
    public class Attachment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the attachment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the attachment.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file name of the attachment, which is saved in the BLOB container.
        /// </summary>
        [Required]
        public string FileName { get; set; } = string.Empty; //The name of the file that is saved in the BLOB container

        /// <summary>
        /// Gets or sets the identifier of the associated application.
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the associated application.
        /// </summary>
        [SwaggerIgnore]
        public virtual Application? Application { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the attachment was last updated.
        /// </summary>
        [SwaggerIgnore]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
