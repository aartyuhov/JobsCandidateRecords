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
    public class AcademicSubjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AcademicSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AcademicSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcademicSubject>>> GetAcademicSubjects()
        {
            return await _context.AcademicSubjects.ToListAsync();
        }

        // GET: api/AcademicSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AcademicSubject>> GetAcademicSubject(int id)
        {
            var academicSubject = await _context.AcademicSubjects.FindAsync(id);

            if (academicSubject == null)
            {
                return NotFound();
            }

            return academicSubject;
        }

        // PUT: api/AcademicSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcademicSubject(int id, AcademicSubject academicSubject)
        {
            if (id != academicSubject.Id)
            {
                return BadRequest();
            }

            _context.Entry(academicSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicSubjectExists(id))
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

        // POST: api/AcademicSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AcademicSubject>> PostAcademicSubject(AcademicSubject academicSubject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AcademicSubjects.Add(academicSubject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcademicSubject", new { id = academicSubject.Id }, academicSubject);
        }

        // DELETE: api/AcademicSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcademicSubject(int id)
        {
            var academicSubject = await _context.AcademicSubjects.FindAsync(id);
            if (academicSubject == null)
            {
                return NotFound();
            }

            _context.AcademicSubjects.Remove(academicSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcademicSubjectExists(int id)
        {
            return _context.AcademicSubjects.Any(e => e.Id == id);
        }
    }
}