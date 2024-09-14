using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing application status history.
    /// </summary>
    /// /// <remarks>
    /// Initializes a new instance of the <see cref="ApplicationStatusHistoryController"/> class.
    /// </remarks>
    /// <param name="userManager">The user manager instance used for user management operations.</param>
    /// <param name="context">The application database context instance.</param>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ApplicationStatusHistoryController(UserManager<IdentityUser> userManager, ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        ///// <summary>
        ///// Initializes a new instance of the <see cref="ApplicationStatusHistoryController"/> class.
        ///// </summary>
        ///// /// <param name="userManager">The application database context.</param>
        ///// <param name="context">The application database context.</param>
        //public ApplicationStatusHistoryController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //}

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
        /// Get application status history by applicationId
        /// </summary>
        [HttpGet("applicationId={applicationId}")]
        public async Task<ActionResult<IEnumerable<ApplicationStatusHistory>>> GetApplicationStatusHistoryByApplicationId(int applicationId)
        {
            var statuses = await _context.ApplicationStatusHistories
                                                .Where(status => status.ApplicationId == applicationId).ToListAsync();
            statuses.ForEach(status =>
            {
                if (status.EmployeeId is null)
                    return;
                var employee = _context.Employees.Find(int.Parse(status.EmployeeId));
                status.EmployeeFullname = $"{employee?.LastName} {employee?.FirstName}";
            });

            return statuses;
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
        public async Task<ActionResult<ApplicationStatusHistory>> PostApplicationStatusHistory(ApplicationStatusDTO applicationStatusDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.FindFirstValue("Id");

            if (userId == null)
            {
                return NotFound("User with Employee wasn't found");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User with Employee wasn't found");
            }

            var employeeId = await _context.Employees
                .Where(e => e.IdentityUserId == userId)
                .Select(e => e.Id).FirstOrDefaultAsync();


            var applicationStatusHistory = new ApplicationStatusHistory
            {
                ApplicationId = applicationStatusDTO.ApplicationId,
                IdentityUserId = userId,
                EmployeeId = employeeId.ToString(),
                ApplicationStatus = applicationStatusDTO.ApplicationStatus,
                DecisionDate = DateTime.Now,
            };


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
