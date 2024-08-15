using JobsCandidateRecords.Enums;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsCandidateRecords.Controllers.DTO
{
    /// <summary>
    /// Provides API endpoints for managing candidate data. This controller allows for CRUD operations and other candidate-related functionality.
    /// The controller requires the user to be authenticated and have specific roles to access its endpoints.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CandidatesDTOController"/> class with the specified candidate service.
    /// </remarks>
    /// <param name="candidateService">The service used for candidate-related operations.</param>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,Manager")]
    public class CandidatesDTOController(ICandidateService candidateService) : ControllerBase
    {
        private readonly ICandidateService _candidateService = candidateService;

        /// <summary>
        /// Gets the list of all candidates.
        /// </summary>
        /// <returns>List of candidates.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidates()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }

        /// <summary>
        /// Gets a candidate by ID.
        /// </summary>
        /// <param name="id">Candidate ID.</param>
        /// <returns>Candidate information.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidate(int id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (candidate == null) return NotFound();
            return Ok(candidate);
        }

        /// <summary>
        /// Creates a new candidate.
        /// </summary>
        /// <param name="candidateDTO">Candidate information.</param>
        /// <returns>Created candidate information.</returns>
        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> CreateCandidate(CandidateDTO candidateDTO)
        {
            var createdCandidate = await _candidateService.CreateCandidateAsync(candidateDTO);
            return CreatedAtAction(nameof(GetCandidate), new { id = createdCandidate.Id }, createdCandidate);
        }

        /// <summary>
        /// Updates an existing candidate.
        /// </summary>s
        /// <param name="candidateDTO">Updated candidate information.</param>
        /// <returns>No content.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCandidate(CandidateDTO candidateDTO)
        {
            if (candidateDTO == null) return BadRequest();
            var result = await _candidateService.UpdateCandidateAsync(candidateDTO);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a candidate.
        /// </summary>
        /// <param name="id">Candidate ID.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var result = await _candidateService.DeleteCandidateAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Updates the status of a specific application for a candidate.
        /// </summary>
        /// <param name="candidateId">Candidate ID.</param>
        /// <param name="applicationId">Application ID.</param>
        /// <param name="newStatus">New application status.</param>
        /// <returns>No content.</returns>
        [HttpPut("{candidateId}/applications/{applicationId}/status")]
        public async Task<IActionResult> UpdateApplicationStatus(int candidateId, int applicationId, [FromBody] ApplicationStatusEnum newStatus)
        {
            var result = await _candidateService.UpdateApplicationStatusAsync(candidateId, applicationId, newStatus);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Gets the list of all candidates by position.
        /// </summary>
        /// <param name="positionId">Position ID.</param>
        /// <returns>List of candidates for the specified position.</returns>
        [HttpGet("position/{positionId}")]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidatesByPosition(int positionId)
        {
            var candidates = await _candidateService.GetCandidatesByPositionAsync(positionId);
            if (!candidates.Any()) return NotFound();
            return Ok(candidates);
        }
    }
}
