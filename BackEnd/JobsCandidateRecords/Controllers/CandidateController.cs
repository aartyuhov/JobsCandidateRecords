using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing candidates.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateController"/> class.
        /// </summary>
        /// <param name="context">The database context to be used.</param>
        public CandidateController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetCandidates.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            return await _context.Candidates
                            .Include(c => c.Applications)
                            .ToListAsync();
        }

        /// <summary>
        /// GetCandidate.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Applications)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate;
        }

        /// <summary>
        /// PutCandidate.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return BadRequest();
            }

            _context.Entry(candidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
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
        /// PostCandidate.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetCandidates), new { id = candidate.Id }, candidate);
        }

        /// <summary>
        /// DeleteCandidate.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.Id == id);
        }
    }
}
