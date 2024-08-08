using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents a note associated with an application and an employee.
    /// </summary>
    [Table("Notes")]
    public class Note
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the application associated with this note.
        /// </summary>
        public int? ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the application associated with this note.
        /// </summary>
        [SwaggerIgnore]
        public virtual Application? Application { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee who created the note.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the employee who created the note.
        /// </summary>
        [SwaggerIgnore]
        public virtual Employee? Employee { get; set; }

        /// <summary>
        /// Gets or sets the text content of the note.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the creation date of the note.
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
