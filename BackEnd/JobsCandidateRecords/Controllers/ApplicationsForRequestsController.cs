using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing applications for requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsForRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationsForRequestsController"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ApplicationsForRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetApplications.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationsForRequests>>> GetApplications()
        {
            return await _context.ApplicationsForRequests
                .ToListAsync();
        }

        /// <summary>
        /// GetApplication.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationsForRequests>> GetApplication(int id)
        {
            var applicationsForRequests = await _context.ApplicationsForRequests
                                                    .FirstOrDefaultAsync(a => a.Id == id);

            if (applicationsForRequests == null)
            {
                return NotFound();
            }

            return applicationsForRequests;
        }

        /// <summary>
        /// PutApplicationsForRequests.
        /// </summary>
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

        /// <summary>
        /// PostApplicationsForRequests.
        /// </summary>
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

        /// <summary>
        /// DeleteApplicationsForRequests.
        /// </summary>
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
