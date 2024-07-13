using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Http;
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

        // GET: api/PositionAcademicSubject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionAcademicSubject>>> GetPositionAcademicSubjects()
        {
            return await _context.PositionAcademicSubjects.ToListAsync();
        }

        // GET: api/PositionAcademicSubject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionAcademicSubject>> GetPositionAcademicSubject(int id)
        {
            var positionAcademicSubject = await _context.PositionAcademicSubjects.FindAsync(id);

            if (positionAcademicSubject == null)
            {
                return NotFound();
            }

            return positionAcademicSubject;
        }

        // PUT: api/PositionAcademicSubject/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/PositionAcademicSubject
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

        // DELETE: api/PositionAcademicSubject/5
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
