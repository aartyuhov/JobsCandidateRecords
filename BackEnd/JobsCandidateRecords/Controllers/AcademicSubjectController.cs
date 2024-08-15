using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing academic subjects.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AcademicSubjectController"/> class.
    /// </remarks>
    /// <param name="context">The database context to be used.</param>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AcademicSubjectController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// GetAcademicSubjects.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcademicSubject>>> GetAcademicSubjects()
        {
            return await _context.AcademicSubjects
                .ToListAsync();
        }

        /// <summary>
        /// GetAcademicSubject.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AcademicSubject>> GetAcademicSubject(int id)
        {
            var academicSubject = await _context.AcademicSubjects
                .FirstOrDefaultAsync(a => a.Id == id);

            if (academicSubject == null)
            {
                return NotFound();
            }

            return academicSubject;
        }

        /// <summary>
        /// PutAcademicSubject.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcademicSubject(int id, AcademicSubject academicSubject)
        {
            if (id != academicSubject.Id)
            {
                return BadRequest();
            }

            _context.Entry(academicSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicSubjectExists(id))
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
        /// PostAcademicSubject.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AcademicSubject>> PostAcademicSubject(AcademicSubject academicSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AcademicSubjects.Add(academicSubject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcademicSubject", new { id = academicSubject.Id }, academicSubject);
        }

        /// <summary>
        /// DeleteAcademicSubject.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicSubject(int id)
        {
            var academicSubject = await _context.AcademicSubjects.FindAsync(id);
            if (academicSubject == null)
            {
                return NotFound();
            }

            _context.AcademicSubjects.Remove(academicSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcademicSubjectExists(int id)
        {
            return _context.AcademicSubjects.Any(e => e.Id == id);
        }
    }
}