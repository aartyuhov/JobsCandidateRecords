using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobsCandidateRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationStatusHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationStatusHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationStatusHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationStatusHistory>>> GetApplicationStatusHistories()
        {
            return await _context.ApplicationStatusHistories.ToListAsync();
        }

        // GET: api/ApplicationStatusHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationStatusHistory>> GetApplicationStatusHistory(int id)
        {
            var applicationStatusHistory = await _context.ApplicationStatusHistories.FindAsync(id);

            if (applicationStatusHistory == null)
            {
                return NotFound();
            }

            return applicationStatusHistory;
        }

        // PUT: api/ApplicationStatusHistory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationStatusHistory(int id, ApplicationStatusHistory applicationStatusHistory)
        {
            if (id != applicationStatusHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationStatusHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationStatusHistoryExists(id))
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

        // POST: api/ApplicationStatusHistory
        [HttpPost]
        public async Task<ActionResult<ApplicationStatusHistory>> PostApplicationStatusHistory(ApplicationStatusHistory applicationStatusHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationStatusHistories.Add(applicationStatusHistory);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetApplicationStatusHistory), new { id = applicationStatusHistory.Id }, applicationStatusHistory);
        }

        // DELETE: api/ApplicationStatusHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationStatusHistory(int id)
        {
            var applicationStatusHistory = await _context.ApplicationStatusHistories.FindAsync(id);
            if (applicationStatusHistory == null)
            {
                return NotFound();
            }
            
            _context.ApplicationStatusHistories.Remove(applicationStatusHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ApplicationStatusHistoryExists(int id)
        {
            return _context.ApplicationStatusHistories.Any(e => e.Id == id);
        }
    }
}
