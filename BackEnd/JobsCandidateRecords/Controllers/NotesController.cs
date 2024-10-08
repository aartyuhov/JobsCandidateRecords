﻿using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// API controller for managing notes.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by this controller.</param>
        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetNotes.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes
                            .ToListAsync();
        }

        /// <summary>
        /// GetNotes.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNotes(int id)
        {
            var notes = await _context.Notes
                                .FirstOrDefaultAsync(e => e.Id == id);

            if (notes == null)
            {
                return NotFound();
            }

            return notes;
        }

        /// <summary>
        /// GetNotesByApplicationId
        /// </summary>
        [HttpGet("{applicationId}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByApplicationId(int applicationId)
        {
            var notes = await _context.Notes.Where(note => note.ApplicationId == applicationId).ToListAsync();

            if (notes == null)
            {
                return NotFound();
            }

            return notes;
        }

        /// <summary>
        /// PutNotes.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes(int id, Position notes)
        {
            if (id != notes.Id)
            {
                return BadRequest();
            }

            _context.Entry(notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// PostNote.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note notes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notes.Add(notes);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetNotes), new { id = notes.Id }, notes);
        }

        /// <summary>
        /// DeleteNote.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
