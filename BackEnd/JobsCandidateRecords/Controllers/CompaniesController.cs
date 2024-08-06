using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetCompanies.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies()
        {
            var companies = await _context.Companies
                .Include(c => c.Departments)
                .ToListAsync();

            var companyDTOs = companies.Select(c => new CompanyDTO(
                c.Id,
                c.Name,
                c.Description
            ));

            return Ok(companyDTOs);
        }

        /// <summary>
        /// GetCompany.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompany(int id)
        {
            var company = await _context.Companies
                .Include(c => c.Departments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            var companyDTO = new CompanyDTO(
                company.Id,
                company.Name,
                company.Description
            );

            return Ok(companyDTO);
        }

        /// <summary>
        /// PutCompany.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, UpdateCompanyDTO updateCompanyDTO)
        {
            if (id != updateCompanyDTO.Id)
            {
                return BadRequest();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            company.Name = updateCompanyDTO.Name;
            company.Description = updateCompanyDTO.Description;

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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
        /// PostCompany.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CompanyDTO>> PostCompany(CreateCompanyDTO createCompanyDTO)
        {
            var company = new Company
            {
                Name = createCompanyDTO.Name,
                Description = createCompanyDTO.Description
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            var companyDTO = new CompanyDTO(
                company.Id,
                company.Name,
                company.Description
            );

            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, companyDTO);
        }

        /// <summary>
        /// DeleteCompany.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
