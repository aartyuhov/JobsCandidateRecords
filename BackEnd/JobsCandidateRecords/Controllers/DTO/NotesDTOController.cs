using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers.DTO
{
    /// <summary>
    /// API controller for managing notes related to applications.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints to create, retrieve, update, and delete notes.
    /// The endpoints are secured and require authentication via JWT tokens.
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="NotesDTOController"/> class.
    /// </remarks>
    /// <param name="noteService">The service to manage notes.</param>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class NotesDTOController(INoteService noteService) : ControllerBase
    {
        private readonly INoteService _noteService = noteService;

        /// <summary>
        /// Retrieves all notes.
        /// </summary>
        /// <returns>A list of all notes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();
            return Ok(notes);
        }

        /// <summary>
        /// Retrieves a specific note by ID.
        /// </summary>
        /// <param name="id">The ID of the note.</param>
        /// <returns>The note if found, otherwise a 404 Not Found response.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDTO>> GetNote(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        /// <summary>
        /// Retrieves all notes for a specific application by application ID.
        /// </summary>
        /// <param name="applicationId">The ID of the application.</param>
        /// <returns>A list of notes for the application.</returns>
        [HttpGet("application/{applicationId}")]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> GetNotesByApplicationId(int applicationId)
        {
            var notes = await _noteService.GetNotesByApplicationIdAsync(applicationId);
            return Ok(notes);
        }

        /// <summary>
        /// Updates an existing note.
        /// </summary>
        /// <param name="noteDTO">The note data transfer object containing updated information.</param>
        /// <returns>An Ok response if the update was successful, otherwise a 404 Not Found response.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateNote(NoteDTO noteDTO)
        {
            if (noteDTO == null) return BadRequest();
            var result = await _noteService.UpdateNoteAsync(noteDTO);
            if (!result) return NotFound();
            return Ok();
        }

        /// <summary>
        /// Creates a new note.
        /// </summary>
        /// <param name="note">The note data transfer object containing information for the new note.</param>
        /// <returns>The created note and its location.</returns>
        [HttpPost]
        public async Task<ActionResult<NoteDTO>> CreateNote(NoteDTO note)
        {
            var createdNote = await _noteService.CreateNoteAsync(note);
            return CreatedAtAction(nameof(GetNote), new { id = createdNote.Id }, createdNote);
        }

        /// <summary>
        /// Deletes a note by ID.
        /// </summary>
        /// <param name="id">The ID of the note to delete.</param>
        /// <returns>An Ok response if the deletion was successful, otherwise a 404 Not Found response.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var result = await _noteService.DeleteNoteAsync(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
