using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing departments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentController"/> class.
        /// </summary>
        /// <param name="context">The database context to be used.</param>
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetDepartments.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments
                            .ToListAsync();
        }

        /// <summary>
        /// GetDepartment.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments
                                        .FirstOrDefaultAsync(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        /// <summary>
        /// PutDepartment.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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
        /// PostDepartment.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }

        /// <summary>
        /// DeleteDepartment.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
