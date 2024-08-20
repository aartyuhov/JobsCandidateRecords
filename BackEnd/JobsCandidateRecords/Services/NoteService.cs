using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Service class responsible for managing notes within the application.
    /// Implements the <see cref="INoteService"/> interface.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="NoteService"/> class with the specified database context.
    /// </remarks>
    /// <param name="context">The <see cref="ApplicationDbContext"/> instance used for database operations.</param>
    public class NoteService(ApplicationDbContext context) : INoteService
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Asynchronously creates a new note in the database.
        /// </summary>
        /// <param name="noteDTO">The data transfer object containing information about the note to be created.</param>
        /// <returns>A task representing the asynchronous operation, containing the created <see cref="NoteDTO"/>.</returns>
        public async Task<NoteDTO> CreateNoteAsync(NoteDTO noteDTO)
        {
            var note = new Note
            {
                ApplicationId = noteDTO.ApplicationId,
                EmployeeId = noteDTO.EmployeeId,
                Text = noteDTO.Text,
                CreationDate = noteDTO.CreationDate
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return new NoteDTO(
                note.Id,
                note.ApplicationId,
                note.EmployeeId,
                note.Text,
                note.CreationDate,
                noteDTO.AuthorName
            );
        }

        /// <summary>
        /// Asynchronously deletes a note from the database by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the note to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, containing a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null) return false;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Asynchronously retrieves all notes from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing a collection of all <see cref="NoteDTO"/> objects.</returns>
        public async Task<IEnumerable<NoteDTO>> GetAllNotesAsync()
        {
            var notes = await _context.Notes
                .Include(n => n.Employee)
                .ToListAsync();

            var noteDTOs = notes.Select(n => new NoteDTO(
                n.Id,
                n.ApplicationId,
                n.EmployeeId,
                n.Text,
                n.CreationDate,
                n.Employee?.FirstName + " " + n.Employee?.LastName
            ));

            return noteDTOs;
        }

        /// <summary>
        /// Asynchronously retrieves a note by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the note to be retrieved.</param>
        /// <returns>A task representing the asynchronous operation, containing the <see cref="NoteDTO"/> object if found; otherwise, null.</returns>
        public async Task<NoteDTO?> GetNoteByIdAsync(int id)
        {
            var note = await _context.Notes
                .Include(n => n.Employee)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null) return null;

            return new NoteDTO(
                note.Id,
                note.ApplicationId,
                note.EmployeeId,
                note.Text,
                note.CreationDate,
                note.Employee?.FirstName + " " + note.Employee?.LastName
            );
        }

        /// <summary>
        /// Asynchronously retrieves all notes associated with a specific application.
        /// </summary>
        /// <param name="applicationId">The unique identifier of the application.</param>
        /// <returns>A task representing the asynchronous operation, containing a collection of <see cref="NoteDTO"/> objects associated with the specified application.</returns>
        public async Task<IEnumerable<NoteDTO>> GetNotesByApplicationIdAsync(int applicationId)
        {
            var notes = await _context.Notes
                .Where(n => n.ApplicationId == applicationId)
                .Include(n => n.Employee)
                .ToListAsync();

            var noteDTOs = notes.Select(n => new NoteDTO(
                n.Id,
                n.ApplicationId,
                n.EmployeeId,
                n.Text,
                n.CreationDate,
                n.Employee?.FirstName + " " + n.Employee?.LastName
            ));

            return noteDTOs;
        }

        /// <summary>
        /// Asynchronously updates an existing note in the database.
        /// </summary>
        /// <param name="noteDTO">The data transfer object containing updated information about the note.</param>
        /// <returns>A task representing the asynchronous operation, containing a boolean value indicating whether the update was successful.</returns>
        public async Task<bool> UpdateNoteAsync(NoteDTO noteDTO)
        {
            var note = await _context.Notes.FindAsync(noteDTO.Id);

            if (note == null) return false;

            note.ApplicationId = noteDTO.ApplicationId;
            note.EmployeeId = noteDTO.EmployeeId;
            note.Text = noteDTO.Text;
            note.CreationDate = noteDTO.CreationDate;

            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
