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
    public class ApplicationsForRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsForRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationsForRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationsForRequests>>> GetApplications()
        {
            return await _context.ApplicationsForRequests
                .Include(a => a.Application)
                .Include(a => a.RequestForEmployee)
                .ToListAsync();
        }

        // GET: api/ApplicationsForRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationsForRequests>> GetApplication(int id)
        {
            var applicationsForRequests = await _context.ApplicationsForRequests
                                                    .Include(a => a.Application)
                                                    .Include(a => a.RequestForEmployee)
                                                    .FirstOrDefaultAsync(a => a.Id == id);

            if (applicationsForRequests == null)
            {
                return NotFound();
            }

            return applicationsForRequests;
        }

        // PUT: api/ApplicationsForRequests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationsForRequests(int id, ApplicationsForRequests applicationsForRequests)
        {
            if (id != applicationsForRequests.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationsForRequests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationsForRequestsExists(id))
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

        // POST: api/ApplicationsForRequests
        [HttpPost]
        public async Task<ActionResult<ApplicationsForRequests>> PostApplicationsForRequests(ApplicationsForRequests applicationsForRequests)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationsForRequests.Add(applicationsForRequests);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetApplication), new { id = applicationsForRequests.Id }, applicationsForRequests);
        }

        // DELETE: api/ApplicationsForRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationsForRequests(int id)
        {
            var applicationsForRequests = await _context.ApplicationsForRequests.FindAsync(id);
            if (applicationsForRequests == null)
            {
                return NotFound();
            }

            _context.ApplicationsForRequests.Remove(applicationsForRequests);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ApplicationsForRequestsExists(int id)
        {
            return _context.ApplicationsForRequests.Any(e => e.Id == id);
        }
    }
}
