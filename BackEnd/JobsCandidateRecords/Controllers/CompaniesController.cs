﻿using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// API controller for managing companies.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CompaniesController"/> class.
    /// </remarks>
    /// <param name="context">The database context.</param>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CompaniesController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

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
            company.Description = updateCompanyDTO.Description ?? string.Empty;

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

            return Ok();
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
                Description = createCompanyDTO.Description ?? string.Empty
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

            return Ok();
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
