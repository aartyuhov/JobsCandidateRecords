using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Service for handling requests related to employees.
    /// </summary>
    public class RequestForEmployeeService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Gets all requests for employees.
        /// </summary>
        /// <returns>A list of RequestForEmployeeDTO objects.</returns>
        public async Task<List<RequestForEmployeeDTO>> GetAllAsync()
        {
            var requests = await _context.RequestsForEmployees
                .Include(r => r.Position)
                .Include(r => r.RequestedEmployee)
                .Include(r => r.ApplicationsForRequests!)
                    .ThenInclude(a => a.Application!)
                        .ThenInclude(app => app.Candidate)
                .ToListAsync();

            return requests.Select(r => new RequestForEmployeeDTO(
                r.Id,
                r.Name,
                r.PublicationDate,
                r.NumberEmployessRequired,
                r.Description,
                r.PositionId,
                r.Position?.Title ?? string.Empty,
                r.RequestedEmployeeId,
                r.RequestedEmployee != null ? $"{r.RequestedEmployee.FirstName} {r.RequestedEmployee.LastName}" : string.Empty,
                r.ApplicationsForRequests?.Select(a =>
                {
                    var application = a.Application;
                    var candidate = application?.Candidate;

                    var statusHistory = application?.ApplicationStatusHistories?.OrderByDescending(s => s.DecisionDate).FirstOrDefault();
                    var applicationStatus = statusHistory != null ? statusHistory.ApplicationStatus.ToString() : string.Empty;

                    return application != null && candidate != null
                        ? new ApplicationDTO(
                            application.Id,
                            new CandidateDTO(
                                candidate.Id,
                                candidate.FirstName,
                                candidate.LastName,
                                candidate.DateOfBirth,
                                candidate.Gender,
                                candidate.Email,
                                candidate.Phone,
                                candidate.Address,
                                candidate.AboutInfo
                            ),
                            application.EmployeeWhoCreatedId,
                            application.CreationDate,
                            application.Details,
                            applicationStatus
                        )
                        : null;
                }).Where(a => a != null).Cast<ApplicationDTO>().ToList() ?? []
            )).ToList();
        }

        /// <summary>
        /// Gets a request for an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the request.</param>
        /// <returns>A RequestForEmployeeDTO object if found, null otherwise.</returns>
        public async Task<RequestForEmployeeDTO?> GetByIdAsync(int id)
        {
            var r = await _context.RequestsForEmployees
                .Include(r => r.Position)
                .Include(r => r.RequestedEmployee)
                .Include(r => r.ApplicationsForRequests!)
                    .ThenInclude(a => a.Application!)
                        .ThenInclude(app => app.Candidate)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (r == null)
            {
                return null;
            }

            return new RequestForEmployeeDTO(
                r.Id,
                r.Name,
                r.PublicationDate,
                r.NumberEmployessRequired,
                r.Description,
                r.PositionId,
                r.Position?.Title ?? string.Empty,
                r.RequestedEmployeeId,
                r.RequestedEmployee != null ? $"{r.RequestedEmployee.FirstName} {r.RequestedEmployee.LastName}" : string.Empty,
                r.ApplicationsForRequests?.Select(a =>
                {
                    var application = a.Application;
                    var candidate = application?.Candidate;

                    var statusHistory = application?.ApplicationStatusHistories?.OrderByDescending(s => s.DecisionDate).FirstOrDefault();
                    var applicationStatus = statusHistory != null ? statusHistory.ApplicationStatus.ToString() : string.Empty;

                    return application != null && candidate != null
                        ? new ApplicationDTO(
                            application.Id,
                            new CandidateDTO(
                                candidate.Id,
                                candidate.FirstName,
                                candidate.LastName,
                                candidate.DateOfBirth,
                                candidate.Gender,
                                candidate.Email,
                                candidate.Phone,
                                candidate.Address,
                                candidate.AboutInfo
                            ),
                            application.EmployeeWhoCreatedId,
                            application.CreationDate,
                            application.Details,
                            applicationStatus
                        )
                        : null;
                }).Where(a => a != null).Cast<ApplicationDTO>().ToList() ?? []
            );
        }

        /// <summary>
        /// Creates a new request for an employee.
        /// </summary>
        /// <param name="dto">The DTO representing the request for an employee.</param>
        /// <returns>The created RequestForEmployeeDTO object.</returns>
        public async Task<RequestForEmployeeDTO> CreateAsync(RequestForEmployeeDTO dto)
        {
            var entity = new RequestForEmployee
            {
                Name = dto.Name,
                PublicationDate = dto.PublicationDate,
                NumberEmployessRequired = dto.NumberEmployessRequired,
                Description = dto.Description,
                PositionId = dto.PositionId,
                RequestedEmployeeId = dto.RequestedEmployeeId
            };

            _context.RequestsForEmployees.Add(entity);
            await _context.SaveChangesAsync();

            return new RequestForEmployeeDTO(
                entity.Id,
                entity.Name,
                entity.PublicationDate,
                entity.NumberEmployessRequired,
                entity.Description,
                entity.PositionId,
                entity.Position != null ? entity.Position.Title : string.Empty,
                entity.RequestedEmployeeId,
                entity.RequestedEmployee != null ? $"{entity.RequestedEmployee.FirstName} {entity.RequestedEmployee.LastName}" : string.Empty,
                []
            );
        }

        /// <summary>
        /// Updates an existing request for an employee.
        /// </summary>
        /// <param name="dto">The DTO representing the updated request for an employee.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateAsync(RequestForEmployeeDTO dto)
        {
            var entity = await _context.RequestsForEmployees.FindAsync(dto.Id);
            if (entity == null)
            {
                return false;
            }

            entity.Name = dto.Name;
            entity.PublicationDate = dto.PublicationDate;
            entity.NumberEmployessRequired = dto.NumberEmployessRequired;
            entity.Description = dto.Description;
            entity.PositionId = dto.PositionId;
            entity.RequestedEmployeeId = dto.RequestedEmployeeId;

            _context.RequestsForEmployees.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes an existing request for an employee.
        /// </summary>
        /// <param name="id">The ID of the request to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.RequestsForEmployees.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.RequestsForEmployees.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Searches for employee requests based on the provided filters.
        /// </summary>
        /// <param name="name">The name to search for (optional).</param>
        /// <param name="positionId">The position ID to search for (optional).</param>
        /// <param name="publicationDate">The publication date to search for (optional).</param>
        /// <returns>A list of matching employee requests.</returns>
        public async Task<List<RequestForEmployeeDTO>> SearchAsync(string? name, int? positionId, DateTime? publicationDate)
        {
            var query = _context.RequestsForEmployees.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(r => r.Name.Contains(name));
            }

            if (positionId.HasValue)
            {
                query = query.Where(r => r.PositionId == positionId.Value);
            }

            if (publicationDate.HasValue)
            {
                query = query.Where(r => r.PublicationDate == publicationDate.Value);
            }

            var requests = await query
                .Include(r => r.Position)
                .Include(r => r.RequestedEmployee)
                .Include(r => r.ApplicationsForRequests!)
                    .ThenInclude(a => a.Application!)
                        .ThenInclude(app => app.Candidate)
                .ToListAsync();

            return requests.Select(r => new RequestForEmployeeDTO(
                r.Id,
                r.Name,
                r.PublicationDate,
                r.NumberEmployessRequired,
                r.Description,
                r.PositionId,
                r.Position?.Title ?? string.Empty,
                r.RequestedEmployeeId,
                r.RequestedEmployee != null ? $"{r.RequestedEmployee.FirstName} {r.RequestedEmployee.LastName}" : string.Empty,
                r.ApplicationsForRequests?.Select(a =>
                {
                    var application = a.Application;
                    var candidate = application?.Candidate;

                    var statusHistory = application?.ApplicationStatusHistories?.OrderByDescending(s => s.DecisionDate).FirstOrDefault();
                    var applicationStatus = statusHistory != null ? statusHistory.ApplicationStatus.ToString() : string.Empty;

                    return application != null && candidate != null
                        ? new ApplicationDTO(
                            application.Id,
                            new CandidateDTO(
                                candidate.Id,
                                candidate.FirstName,
                                candidate.LastName,
                                candidate.DateOfBirth,
                                candidate.Gender,
                                candidate.Email,
                                candidate.Phone,
                                candidate.Address,
                                candidate.AboutInfo
                            ),
                            application.EmployeeWhoCreatedId,
                            application.CreationDate,
                            application.Details,
                            applicationStatus
                        )
                        : null;
                }).Where(a => a != null).Cast<ApplicationDTO>().ToList() ?? []
            )).ToList();
        }

        /// <summary>
        /// Sends notifications to candidates associated with the specified employee request.
        /// </summary>
        /// <param name="requestId">The ID of the request for which to send notifications.</param>
        /// <returns>True if the request was found and notifications were sent, false otherwise.</returns>
        public async Task<bool> SendNotificationsAsync(int requestId)
        {
            var request = await _context.RequestsForEmployees
                .Include(r => r.ApplicationsForRequests!)
                    .ThenInclude(a => a.Application!)
                        .ThenInclude(app => app.Candidate)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
            {
                return false;
            }

            //Logic for sending notifications
            //Example:
            //foreach (var applicationForRequest in request.ApplicationsForRequests)
            //{
            //    var candidate = applicationForRequest.Application?.Candidate;
            //    if (candidate != null)
            //    {
            //        SendEmail(candidate.Email, "New Application Received", "Your application has been received.");
            //    }
            //}

            return true;
        }

        /// <summary>
        /// Updates the status of multiple applications.
        /// </summary>
        /// <param name="updateStatusDTO">The DTO containing application IDs and the new status.</param>
        /// <returns>True if the applications were found and updated, false otherwise.</returns>
        public async Task<bool> UpdateApplicationStatusAsync(UpdateStatusDTO updateStatusDTO)
        {
            var applications = await _context.Applications
                .Where(a => updateStatusDTO.ApplicationIds.Contains(a.Id))
                .ToListAsync();

            if (applications.Count == 0)
            {
                return false;
            }

            foreach (var application in applications)
            {
                var statusHistory = new ApplicationStatusHistory
                {
                    ApplicationId = application.Id,
                    ApplicationStatus = updateStatusDTO.NewStatus,
                    DecisionDate = DateTime.Now
                };

                _context.ApplicationStatusHistories.Add(statusHistory);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
