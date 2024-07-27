using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionAcademicSubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PositionAcademicSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetPositionAcademicSubjects.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionAcademicSubject>>> GetPositionAcademicSubjects()
        {
            return await _context.PositionAcademicSubjects
                            .Include(p => p.Position)
                            .Include(p => p.AcademicSubject)
                            .ToListAsync();
        }

        /// <summary>
        /// GetPositionAcademicSubject.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionAcademicSubject>> GetPositionAcademicSubject(int id)
        {
            var positionAcademicSubject = await _context.PositionAcademicSubjects
                                                    .Include(p => p.Position)
                                                    .Include(p => p.AcademicSubject)
                                                    .FirstOrDefaultAsync(p => p.Id == id);

            if (positionAcademicSubject == null)
            {
                return NotFound();
            }

            return positionAcademicSubject;
        }

        /// <summary>
        /// PutPosition.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, PositionAcademicSubject positionAcademicSubject)
        {
            if (id != positionAcademicSubject.Id)
            {
                return BadRequest();
            }

            _context.Entry(positionAcademicSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionAcademicSubjectExists(id))
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
        /// PostpositionAcademicSubject.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Position>> PostpositionAcademicSubject(PositionAcademicSubject positionAcademicSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PositionAcademicSubjects.Add(positionAcademicSubject);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetPositionAcademicSubject), new { id = positionAcademicSubject.Id }, positionAcademicSubject);
        }

        /// <summary>
        /// DeletePositionAcademicSubject.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionAcademicSubject(int id)
        {
            var positionAcademicSubject = await _context.PositionAcademicSubjects.FindAsync(id);
            if (positionAcademicSubject == null)
            {
                return NotFound();
            }

            _context.PositionAcademicSubjects.Remove(positionAcademicSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PositionAcademicSubjectExists(int id)
        {
            return _context.PositionAcademicSubjects.Any(e => e.Id == id);
        }
    }
}
