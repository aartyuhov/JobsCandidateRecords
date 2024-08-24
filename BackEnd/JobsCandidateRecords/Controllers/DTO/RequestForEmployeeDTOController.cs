using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobsCandidateRecords.Controllers.DTO
{
    /// <summary>
    /// Controller for managing requests for employees.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RequestForEmployeeDTOController(IRequestForEmployeeService service) : ControllerBase
    {
        private readonly IRequestForEmployeeService _service = service;

        /// <summary>
        /// Gets all employee requests.
        /// </summary>
        /// <returns>A list of employee requests.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestForEmployeeDTO>>> GetRequests()
        {
            var requests = await _service.GetAllAsync();
            return Ok(requests);
        }

        /// <summary>
        /// Gets an employee request by id.
        /// </summary>
        /// <param name="id">The id of the employee request.</param>
        /// <returns>The employee request with the specified id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestForEmployeeDTO>> GetRequest(int id)
        {
            var request = await _service.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        /// <summary>
        /// Creates a new employee request.
        /// </summary>
        /// <param name="dto">The employee request data transfer object.</param>
        /// <returns>The created employee request.</returns>
        [HttpPost]
        public async Task<ActionResult<RequestForEmployeeDTO>> CreateRequest(RequestForEmployeeDTO dto)
        {
            var createdRequest = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetRequest), new { id = createdRequest.Id }, createdRequest);
        }

        /// <summary>
        /// Updates an existing employee request.
        /// </summary>
        /// <param name="dto">The employee request data transfer object.</param>
        /// <returns>A no-content result if the update was successful.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRequest(RequestForEmployeeDTO dto)
        {
            var result = await _service.UpdateAsync(dto);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Deletes an employee request by id.
        /// </summary>
        /// <param name="id">The id of the employee request to delete.</param>
        /// <returns>A no-content result if the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Searches for employee requests based on provided criteria.
        /// </summary>
        /// <param name="name">The name of the request.</param>
        /// <param name="positionId">The ID of the position.</param>
        /// <param name="publicationDate">The publication date of the request.</param>
        /// <returns>A list of matching employee requests.</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<RequestForEmployeeDTO>>> SearchRequests(string? name, int? positionId, DateTime? publicationDate)
        {
            var requests = await _service.SearchAsync(name, positionId, publicationDate);
            return Ok(requests);
        }

        /// <summary>
        /// Sends notifications for new applications for a request.
        /// </summary>
        /// <param name="requestId">The ID of the request.</param>
        /// <returns>A no-content result if the notifications were sent successfully.</returns>
        [HttpPost("{requestId}/sendNotifications")]
        public async Task<IActionResult> SendNotifications(int requestId)
        {
            var result = await _service.SendNotificationsAsync(requestId);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Updates the status of multiple applications.
        /// </summary>
        /// <param name="updateStatusDTO">The DTO containing the application IDs and the new status.</param>
        /// <returns>A no-content result if the update was successful.</returns>
        [HttpPut("updateapplicationStatus")]
        public async Task<IActionResult> UpdateApplicationStatus(UpdateApplicationStatusDTO updateStatusDTO)
        {
            var result = await _service.UpdateApplicationStatusAsync(updateStatusDTO);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Updates the status of multiple applications.
        /// </summary>
        /// <param name="updateStatusDTO">The DTO containing the application IDs and the new status.</param>
        /// <returns>A no-content result if the update was successful.</returns>
        [HttpPut("updateRequestStatus")]
        public async Task<IActionResult> UpdateRequestStatus(UpdateRequestStatusDTO updateStatusDTO)
        {
            var result = await _service.UpdateRequestStatusAsync(updateStatusDTO);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
