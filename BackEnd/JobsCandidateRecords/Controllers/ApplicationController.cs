using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// GetApplications.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return await _context.Applications
                .Include(a => a.Candidate)
                .Include(a => a.EmployeeWhoCreated)
                .Include(a => a.ApplicationsForRequests)
                .Include(a => a.ApplicationStatusHistories)
                .Include(a => a.Notes)
                .Include(a => a.Attachments)
                .ToListAsync();
        }

        /// <summary>
        /// GetApplication.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _context.Applications
                .Include(a => a.Candidate)
                .Include(a => a.EmployeeWhoCreated)
                .Include(a => a.ApplicationsForRequests)
                .Include(a => a.ApplicationStatusHistories)
                .Include(a => a.Notes)
                .Include(a => a.Attachments)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        /// <summary>
        /// PutApplication.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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
        /// PostApplication.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, application);
        }

        /// <summary>
        /// DeleteApplication.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
