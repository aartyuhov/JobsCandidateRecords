using JobsCandidateRecords.Data;
using JobsCandidateRecords.Enums;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Provides services related to candidates, including operations for retrieving, creating, updating, and deleting candidate information.
    /// Implements the <see cref="ICandidateService"/> interface to define the operations available for candidate management.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CandidateService"/> class with the specified database context.
    /// </remarks>
    /// <param name="context">The database context used to interact with the data store for candidate-related operations.</param>
    public class CandidateService(ApplicationDbContext context) : ICandidateService
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Asynchronously retrieves all candidates along with their associated applications and application status histories.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a collection of <see cref="CandidateDTO"/> objects.</returns>
        public async Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync()
        {
            // Retrieve candidates and include necessary navigation properties
            var candidates = await _context.Candidates
                .Include(c => c.Applications)
                    .ThenInclude(a => a.ApplicationStatusHistories)
                .Include(a => a.Applications)
                    .ThenInclude(a => a.ApplicationsForRequests)
                        .ThenInclude(ar => ar.RequestForEmployee)
                .ToListAsync();

            // Transform candidates into CandidateDTOs
            var candidateDTOs = candidates.Select(c => new CandidateDTO(
                c.Id,
                c.FirstName,
                c.LastName,
                c.DateOfBirth,
                c.Gender,
                c.Email,
                c.Phone,
                c.Address,
                c.AboutInfo,
                c.Applications?.Select(a => new ApplicationStatusDTO(
                    a.Id,
                    a.ApplicationsForRequests?.Count > 0
                        ? a.ApplicationsForRequests.First().RequestForEmployee?.Name ?? string.Empty
                        : string.Empty,
                    a.ApplicationStatusHistories?.Count > 0
                        ? a.ApplicationStatusHistories
                            .OrderByDescending(s => s.DecisionDate)
                            .FirstOrDefault()?.ApplicationStatus ?? ApplicationStatusEnum.NotSelected
                        : ApplicationStatusEnum.NotSelected
                ))?.ToList() ?? []
            ));

            return candidateDTOs;
        }



        /// <summary>
        /// Asynchronously retrieves a candidate by their unique identifier along with their applications and application status histories.
        /// </summary>
        /// <param name="id">The unique identifier of the candidate.</param>
        /// <returns>A task that represents the asynchronous operation, containing the <see cref="CandidateDTO"/> object if found; otherwise, null.</returns>
        public async Task<CandidateDTO?> GetCandidateByIdAsync(int id)
        {
            var candidate = await _context.Candidates
                .Include(c => c.Applications)
                    .ThenInclude(a => a.ApplicationStatusHistories)
                .Include(a => a.Applications)
                    .ThenInclude(a => a.ApplicationsForRequests)
                        .ThenInclude(ar => ar.RequestForEmployee)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (candidate == null) return null;

            return new CandidateDTO(
                candidate.Id,
                candidate.FirstName,
                candidate.LastName,
                candidate.DateOfBirth,
                candidate.Gender,
                candidate.Email,
                candidate.Phone,
                candidate.Address,
                candidate.AboutInfo,
                candidate.Applications?.Select(a => new ApplicationStatusDTO(
                    a.Id,
                    (a.ApplicationsForRequests?.Count != 0) == true
                        ? a.ApplicationsForRequests?.First().RequestForEmployee?.Name ?? string.Empty
                        : string.Empty,
                    a.ApplicationStatusHistories != null && a.ApplicationStatusHistories.Count != 0
                        ? a.ApplicationStatusHistories
                            .OrderByDescending(s => s.DecisionDate)
                            .Select(s => s.ApplicationStatus)
                            .FirstOrDefault()
                        : ApplicationStatusEnum.NotSelected
                ))?.ToList() ?? []
            );
        }

        /// <summary>
        /// Asynchronously creates a new candidate and adds it to the data store.
        /// </summary>
        /// <param name="candidateDTO">The data transfer object containing information about the candidate to create.</param>
        /// <returns>A task that represents the asynchronous operation, containing the newly created <see cref="CandidateDTO"/> object.</returns>
        public async Task<CandidateDTO> CreateCandidateAsync(CandidateDTO candidateDTO)
        {
            var candidate = new Candidate
            {
                FirstName = candidateDTO.FirstName,
                LastName = candidateDTO.LastName,
                DateOfBirth = candidateDTO.DateOfBirth,
                Gender = candidateDTO.Gender,
                Email = candidateDTO.Email,
                Phone = candidateDTO.Phone,
                Address = candidateDTO.Address,
                AboutInfo = candidateDTO.AboutInfo
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            // Assuming no applications are created at candidate creation
            return new CandidateDTO(
                candidate.Id,
                candidate.FirstName,
                candidate.LastName,
                candidate.DateOfBirth,
                candidate.Gender,
                candidate.Email,
                candidate.Phone,
                candidate.Address,
                candidate.AboutInfo,
                []
            );
        }

        /// <summary>
        /// Asynchronously updates an existing candidate's details in the data store.
        /// </summary>
        /// <param name="candidateDTO">The data transfer object containing the updated candidate information.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating whether the update was successful.</returns>
        public async Task<bool> UpdateCandidateAsync(CandidateDTO candidateDTO)
        {
            var candidate = await _context.Candidates.FindAsync(candidateDTO.Id);

            if (candidate == null) return false;

            candidate.FirstName = candidateDTO.FirstName;
            candidate.LastName = candidateDTO.LastName;
            candidate.DateOfBirth = candidateDTO.DateOfBirth;
            candidate.Gender = candidateDTO.Gender;
            candidate.Email = candidateDTO.Email;
            candidate.Phone = candidateDTO.Phone;
            candidate.Address = candidateDTO.Address;
            candidate.AboutInfo = candidateDTO.AboutInfo;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Asynchronously deletes a candidate from the data store by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the candidate to delete.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating whether the deletion was successful.</returns>
        public async Task<bool> DeleteCandidateAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);

            if (candidate == null) return false;

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Asynchronously updates the status of multiple applications.
        /// </summary>
        /// <param name="updateStatusDto">The DTO containing application IDs and the new status.</param>
        /// <returns>A task that represents the asynchronous operation, containing a boolean value indicating whether the update was successful.</returns>
        public async Task<bool> UpdateApplicationStatusAsync(UpdateApplicationStatusDTO updateStatusDto)
        {
            var applications = await _context.Applications
                .Where(a => updateStatusDto.ApplicationIds.Contains(a.Id))
                .Include(a => a.ApplicationStatusHistories)
                .ToListAsync();

            if (applications == null || applications.Count == 0) return false;

            foreach (var application in applications)
            {
                var statusHistory = new ApplicationStatusHistory
                {
                    ApplicationId = application.Id,
                    ApplicationStatus = updateStatusDto.NewStatus,
                    DecisionDate = DateTime.Now
                };

                _context.ApplicationStatusHistories.Add(statusHistory);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Asynchronously retrieves candidates by their position ID, including their applications and application status histories.
        /// </summary>
        /// <param name="positionId">The unique identifier of the position to filter candidates by.</param>
        /// <returns>A task that represents the asynchronous operation, containing a collection of <see cref="CandidateDTO"/> objects.</returns>
        public async Task<IEnumerable<CandidateDTO>> GetCandidatesByPositionAsync(int positionId)
        {
            var candidates = await _context.Candidates
                .Where(c => c.Applications
                    .Any(a => a.ApplicationsForRequests
                        .Any(ar => ar.RequestForEmployee.PositionId == positionId)))
                .Include(c => c.Applications)
                    .ThenInclude(a => a.ApplicationStatusHistories)
                    .Include(ash => ash.Applications)
                        .ThenInclude(a => a.ApplicationsForRequests)
                            .ThenInclude(ar => ar.RequestForEmployee)
                .ToListAsync();

            var candidateDTOs = candidates.Select(c => new CandidateDTO(
                c.Id,
                c.FirstName,
                c.LastName,
                c.DateOfBirth,
                c.Gender,
                c.Email,
                c.Phone,
                c.Address,
                c.AboutInfo,
                c.Applications?.Select(app => new ApplicationStatusDTO(
                    app.Id,
                    app.ApplicationsForRequests?.Where(r => r.RequestForEmployee != null && r.RequestForEmployee.PositionId == positionId)
                        .Select(r => r.RequestForEmployee?.Name)
                        .FirstOrDefault() ?? "Unknown",
                    app.ApplicationStatusHistories != null && app.ApplicationStatusHistories.Count != 0
                        ? app.ApplicationStatusHistories
                            .OrderByDescending(ash => ash.DecisionDate)
                            .Select(ash => ash.ApplicationStatus)
                            .FirstOrDefault()
                        : ApplicationStatusEnum.NotSelected
                ))?.ToList() ?? []
            ));

            return candidateDTOs;
        }
    }
}
