using JobsCandidateRecords.Enums;
using JobsCandidateRecords.Models.DTO;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Defines the contract for candidate-related operations in the application.
    /// </summary>
    public interface ICandidateService
    {
        /// <summary>
        /// Asynchronously retrieves all candidates.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a collection of <see cref="CandidateDTO"/> objects.</returns>
        Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync();

        /// <summary>
        /// Asynchronously retrieves a candidate by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the candidate.</param>
        /// <returns>A task that represents the asynchronous operation, containing the <see cref="CandidateDTO"/> object if found, otherwise null.</returns>
        Task<CandidateDTO?> GetCandidateByIdAsync(int id);

        /// <summary>
        /// Asynchronously creates a new candidate.
        /// </summary>
        /// <param name="candidateDTO">The candidate data transfer object containing the information for the new candidate.</param>
        /// <returns>A task that represents the asynchronous operation, containing the created <see cref="CandidateDTO"/> object.</returns>
        Task<CandidateDTO> CreateCandidateAsync(CandidateDTO candidateDTO);

        /// <summary>
        /// Asynchronously updates an existing candidate.
        /// </summary>
        /// <param name="candidateDTO">The candidate data transfer object containing the updated information.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating whether the update was successful.</returns>
        Task<bool> UpdateCandidateAsync(CandidateDTO candidateDTO);

        /// <summary>
        /// Asynchronously deletes a candidate by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the candidate to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating whether the deletion was successful.</returns>
        Task<bool> DeleteCandidateAsync(int id);

        /// <summary>
        /// Asynchronously updates the application status of a candidate.
        /// </summary>
        /// <param name="candidateId">The unique identifier of the candidate.</param>
        /// <param name="applicationId">The unique identifier of the application.</param>
        /// <param name="newStatus">The new status to be set for the application.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating whether the update was successful.</returns>
        Task<bool> UpdateApplicationStatusAsync(int candidateId, int applicationId, ApplicationStatusEnum newStatus);

        /// <summary>
        /// Asynchronously retrieves candidates by their position ID.
        /// </summary>
        /// <param name="positionId">The unique identifier of the position.</param>
        /// <returns>A task that represents the asynchronous operation, containing a collection of <see cref="CandidateDTO"/> objects that match the specified position.</returns>
        Task<IEnumerable<CandidateDTO>> GetCandidatesByPositionAsync(int positionId);
    }

}
