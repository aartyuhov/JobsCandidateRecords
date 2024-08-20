using Swashbuckle.AspNetCore.Annotations;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) representing a note associated with an application or employee.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="NoteDTO"/> class.
    /// </remarks>
    /// <param name="id">The unique identifier for the note.</param>
    /// <param name="applicationId">The identifier of the associated application, which can be null.</param>
    /// <param name="employeeId">The identifier of the employee associated with the note.</param>
    /// <param name="text">The content of the note.</param>
    /// <param name="creationDate">The date and time when the note was created.</param>
    /// <param name="authorName">The name of the author who created the note.</param>
    public class NoteDTO(int id, int? applicationId, int employeeId, string text, DateTime creationDate, string authorName)
    {
        /// <summary>
        /// Gets or sets the unique identifier for the note.
        /// </summary>
        public int Id { get; set; } = id;

        /// <summary>
        /// Gets or sets the identifier of the associated application. 
        /// This is nullable, as a note may or may not be linked to a specific application.
        /// </summary>
        public int? ApplicationId { get; set; } = applicationId;

        /// <summary>
        /// Gets or sets the identifier of the employee associated with the note.
        /// </summary>
        public int EmployeeId { get; set; } = employeeId;

        /// <summary>
        /// Gets or sets the content of the note.
        /// </summary>
        /// <value>
        /// The text of the note. Defaults to an empty string.
        /// </value>
        public string Text { get; set; } = text;

        /// <summary>
        /// Gets or sets the date and time when the note was created.
        /// </summary>
        /// <value>
        /// The creation date of the note. Defaults to the current date and time.
        /// </value>
        public DateTime CreationDate { get; set; } = creationDate;

        /// <summary>
        /// Gets or sets the name of the author who created the note.
        /// </summary>
        /// <value>
        /// The name of the note's author. Defaults to an empty string.
        /// </value>
        public string AuthorName { get; set; } = authorName;
    }
}
