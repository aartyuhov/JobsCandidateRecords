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
    /// Represents the database context for the application.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                .ToListAsync();
        }

        /// <summary>
        /// GetApplication.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _context.Applications
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
        public async Task<ActionResult<ApplicationDTO>> PostApplication(CreateApplicationDTO createApplicationDto, int requestForEmployeeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var candidate = await _context.Candidates.FindAsync(createApplicationDto.CandidateId);
            if (candidate == null)
            {
                return NotFound("Candidate not found.");
            }

            var application = new Application
            {
                CandidateId = createApplicationDto.CandidateId,
                Candidate = candidate, 
                EmployeeWhoCreatedId = createApplicationDto.EmployeeWhoCreatedId,
                CreationDate = createApplicationDto.CreationDate,
                Details = createApplicationDto.Details,
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            var applicationsForRequests = new ApplicationsForRequests
            {
                ApplicationId = application.Id,
                RequestForEmployeeId = requestForEmployeeId
            };
            _context.ApplicationsForRequests.Add(applicationsForRequests);
            await _context.SaveChangesAsync();


            var applicationStatusHistory = new ApplicationStatusHistory
            {
                ApplicationId = application.Id,
                ApplicationStatus = Enums.ApplicationStatusEnum.New,
                DecisionDate = DateTime.UtcNow
            };
            _context.ApplicationStatusHistories.Add(applicationStatusHistory);
            await _context.SaveChangesAsync();

            var candidateDto = new CandidateDTO(
                candidate.Id,
                candidate.FirstName,
                candidate.LastName,
                candidate.DateOfBirth,
                candidate.Gender,
                candidate.Email,
                candidate.Phone,
                candidate.Address,
                candidate.AboutInfo,
                new List<ApplicationStatusDTO>()
            );

            var applicationDto = new ApplicationDTO(
                application.Id,
                candidateDto,
                application.EmployeeWhoCreatedId,
                application.CreationDate,
                application.Details,
                Enums.ApplicationStatusEnum.New.ToString() 
            );

            return CreatedAtAction(nameof(GetApplication), new { id = application.Id }, applicationDto);
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
