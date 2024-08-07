using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing application status history.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ApplicationStatusHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationStatusHistoryController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ApplicationStatusHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetApplicationStatusHistories.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationStatusHistory>>> GetApplicationStatusHistories()
        {
            return await _context.ApplicationStatusHistories
                .ToListAsync();
        }

        /// <summary>
        /// GetApplicationStatusHistory.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationStatusHistory>> GetApplicationStatusHistory(int id)
        {
            var applicationStatusHistory = await _context.ApplicationStatusHistories
                                                    .FirstOrDefaultAsync(a => a.Id == id);

            if (applicationStatusHistory == null)
            {
                return NotFound();
            }

            return applicationStatusHistory;
        }

        /// <summary>
        /// PutApplicationStatusHistory.
        /// </summary>
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

        /// <summary>
        /// PostApplicationStatusHistory.
        /// </summary>
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

        /// <summary>
        /// DeleteApplicationStatusHistory.
        /// </summary>
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
