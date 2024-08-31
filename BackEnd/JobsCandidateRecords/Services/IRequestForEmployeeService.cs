using JobsCandidateRecords.Models.DTO;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Defines the contract for the employee request service.
    /// </summary>
    public interface IRequestForEmployeeService
    {
        /// <summary>
        /// Gets all requests for employees.
        /// </summary>
        /// <returns>A list of RequestForEmployeeDTO objects.</returns>
        Task<List<RequestForEmployeeDTO>> GetAllAsync();

        /// <summary>
        /// Gets a request for an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the request.</param>
        /// <returns>A RequestForEmployeeDTO object if found, null otherwise.</returns>
        Task<RequestForEmployeeDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new request for an employee.
        /// </summary>
        /// <param name="dto">The DTO representing the request for an employee.</param>
        /// <returns>The created RequestForEmployeeDTO object.</returns>
        Task<RequestForEmployeeDTO> CreateAsync(RequestForEmployeeDTO dto);

        /// <summary>
        /// Updates an existing request for an employee.
        /// </summary>
        /// <param name="dto">The DTO representing the updated request for an employee.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateAsync(RequestForEmployeeDTO dto);

        /// <summary>
        /// Deletes an existing request for an employee.
        /// </summary>
        /// <param name="id">The ID of the request to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Searches for employee requests based on the provided filters.
        /// </summary>
        /// <param name="name">The name to search for (optional).</param>
        /// <param name="positionId">The position ID to search for (optional).</param>
        /// <param name="publicationDate">The publication date to search for (optional).</param>
        /// <returns>A list of matching employee requests.</returns>
        Task<List<RequestForEmployeeDTO>> SearchAsync(string? name, int? positionId, DateTime? publicationDate);

        /// <summary>
        /// Sends notifications to candidates associated with the specified employee request.
        /// </summary>
        /// <param name="requestId">The ID of the request for which to send notifications.</param>
        /// <returns>True if the request was found and notifications were sent, false otherwise.</returns>
        Task<bool> SendNotificationsAsync(int requestId);

        /// <summary>
        /// Asynchronously updates the status of a request for an employee.
        /// </summary>
        /// <param name="updateRequestStatusDTO">An object containing the details necessary to update the request status, 
        /// including the request ID and the new status to be applied.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a boolean indicating whether the update 
        /// was successful (true) or not (false).
        /// </returns>
        Task<bool> UpdateRequestStatusAsync(UpdateRequestStatusDTO updateRequestStatusDTO);

        /// <summary>
        /// Updates the status of multiple applications.
        /// </summary>
        /// <param name="updateStatusDTO">The DTO containing application IDs and the new status.</param>
        /// <returns>True if the applications were found and updated, false otherwise.</returns>
        Task<bool> UpdateApplicationStatusAsync(UpdateApplicationStatusDTO updateStatusDTO);
    }
}
