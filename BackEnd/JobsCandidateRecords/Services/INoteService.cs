using JobsCandidateRecords.Models.DTO;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Interface for the note service, defining the operations for managing notes within the system.
    /// </summary>
    public interface INoteService
    {
        /// <summary>
        /// Asynchronously retrieves all notes.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing a collection of all <see cref="NoteDTO"/> objects.</returns>
        Task<IEnumerable<NoteDTO>> GetAllNotesAsync();

        /// <summary>
        /// Asynchronously retrieves a specific note by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the note.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the <see cref="NoteDTO"/> object if found; 
        /// otherwise, null if the note does not exist.
        /// </returns>
        Task<NoteDTO?> GetNoteByIdAsync(int id);

        /// <summary>
        /// Asynchronously retrieves all notes associated with a specific application.
        /// </summary>
        /// <param name="applicationId">The unique identifier of the application.</param>
        /// <returns>A task representing the asynchronous operation, containing a collection of <see cref="NoteDTO"/> objects associated with the specified application.</returns>
        Task<IEnumerable<NoteDTO>> GetNotesByApplicationIdAsync(int applicationId);

        /// <summary>
        /// Asynchronously creates a new note.
        /// </summary>
        /// <param name="noteDTO">The data transfer object representing the note to be created.</param>
        /// <returns>A task representing the asynchronous operation, containing the created <see cref="NoteDTO"/> object.</returns>
        Task<NoteDTO> CreateNoteAsync(NoteDTO noteDTO);

        /// <summary>
        /// Asynchronously updates an existing note.
        /// </summary>
        /// <param name="noteDTO">The data transfer object representing the note to be updated.</param>
        /// <returns>A task representing the asynchronous operation, containing a boolean value indicating whether the update was successful.</returns>
        Task<bool> UpdateNoteAsync(NoteDTO noteDTO);

        /// <summary>
        /// Asynchronously deletes a specific note by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the note to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, containing a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteNoteAsync(int id);
    }
}
