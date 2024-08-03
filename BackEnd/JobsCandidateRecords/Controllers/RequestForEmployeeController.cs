using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing requests for employees.
    /// </summary>
    /// <param name="context">The database context.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class RequestForEmployeeController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// GetRequestForEmployees.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestForEmployee>>> GetRequestForEmployees()
        {
            return await _context.RequestsForEmployees
                            .ToListAsync();
        }

        /// <summary>
        /// GetRequestForEmployee.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestForEmployee>> GetRequestForEmployee(int id)
        {
            var requestForEmployee = await _context.RequestsForEmployees
                                                .FirstOrDefaultAsync(r => r.Id == id);

            if (requestForEmployee == null)
            {
                return NotFound();
            }

            return requestForEmployee;
        }

        /// <summary>
        /// PutRequestForEmployee.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestForEmployee(int id, RequestForEmployee requestForEmployee)
        {
            if (id != requestForEmployee.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestForEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestForEmployeeExists(id))
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
        public async Task<ActionResult<Position>> PostpositionAcademicSubject(RequestForEmployee requestForEmployee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RequestsForEmployees.Add(requestForEmployee);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetRequestForEmployee), new { id = requestForEmployee.Id }, requestForEmployee);
        }

        /// <summary>
        /// DeleteRequestForEmployee.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestForEmployee(int id)
        {
            var requestForEmployee = await _context.RequestsForEmployees.FindAsync(id);
            if (requestForEmployee == null)
            {
                return NotFound();
            }

            _context.RequestsForEmployees.Remove(requestForEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool RequestForEmployeeExists(int id)
        {
            return _context.RequestsForEmployees.Any(e => e.Id == id);
        }
    }
}
