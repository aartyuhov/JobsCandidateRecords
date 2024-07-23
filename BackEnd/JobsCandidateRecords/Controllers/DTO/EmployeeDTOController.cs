using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers.DTO
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDTOController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDTOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Преобразование Employee в EmployeeDTO
        private static EmployeeDTO EmployeeToDTO(Employee employee)
        {
            return new EmployeeDTO(
                employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.DateOfBirth,
                employee.Gender,
                employee.Email,
                employee.PhoneNumber,
                employee.Address,
                employee.HireDate,
                employee.Position?.Title,
                employee.AvatarUrl
            );
        }

        // GET: api/EmployeeDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.Employees
                                    .Include(e => e.Position)
                                    .ToListAsync();

            var employeeDTOs = employees.Select(e => EmployeeToDTO(e)).ToList();

            return Ok(employeeDTOs);
        }

        // GET: api/EmployeeDTO/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                                    .Include(e => e.Position)
                                    .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(EmployeeToDTO(employee));
        }

        // PUT: api/EmployeeDTO
        [HttpPut]
        public async Task<IActionResult> PutEmployee(Employee employee)
        {
            if (!EmployeeExists(employee.Id))
            {
                return NotFound();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
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

        // POST: api/EmployeeDTO
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employeeDTO = EmployeeToDTO(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employeeDTO.Id }, employeeDTO);
        }

        // DELETE: api/EmployeeDTO/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
