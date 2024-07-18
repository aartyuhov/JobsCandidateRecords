using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestForEmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestForEmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestForEmployee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestForEmployee>>> GetRequestForEmployees()
        {
            return await _context.RequestsForEmployees
                            .Include(r => r.Position)
                            .Include(r => r.RequestedEmployee)
                            .Include(r => r.ApplicaionsForRequests)
                            .ToListAsync();
        }

        // GET: api/RequestForEmployee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestForEmployee>> GetRequestForEmployee(int id)
        {
            var requestForEmployee = await _context.RequestsForEmployees
                                                .Include(r => r.Position)
                                                .Include(r => r.RequestedEmployee)
                                                .Include(r => r.ApplicaionsForRequests)
                                                .FirstOrDefaultAsync(r => r.Id == id);

            if (requestForEmployee == null)
            {
                return NotFound();
            }

            return requestForEmployee;
        }

        // PUT: api/RequestForEmployee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/RequestForEmployee
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

        // DELETE: api/RequestForEmployee/5
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
