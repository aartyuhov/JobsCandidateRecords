using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetPositions.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            return await _context.Positions
                            .Include(p => p.Department)
                            .Include(p => p.Employees)
                            .Include(p => p.RequestForEmployees)
                            .Include(p => p.PositionAcademicSubjects)
                            .ToListAsync();
        }

        /// <summary>
        /// GetPosition.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
            var notes = await _context.Positions
                                .Include(p => p.Department)
                                .Include(p => p.Employees)
                                .Include(p => p.RequestForEmployees)
                                .Include(p => p.PositionAcademicSubjects)
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (notes == null)
            {
                return NotFound();
            }

            return notes;
        }

        /// <summary>
        /// PutPosition.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, Position position)
        {
            if (id != position.Id)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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
        public async Task<ActionResult<Position>> PostNote(Position position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Positions.Add(position);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
        }

        /// <summary>
        /// DeletePosition.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}
