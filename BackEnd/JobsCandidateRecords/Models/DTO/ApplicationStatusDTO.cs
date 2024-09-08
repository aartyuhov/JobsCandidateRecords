using JobsCandidateRecords.Enums;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for application status information.
    /// </summary>
    public record ApplicationStatusDTO
    {
        /// <summary>
        /// Identifier of the application.
        /// </summary>
        public int ApplicationId { get; init; }

        /// <summary>
        /// Gets or sets the ID of the identity user associated with the creating application status hystory.
        /// </summary>
        public string? IdentityUserId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee associated with the creating application status hystory.
        /// </summary>
        public string? EmployeeId { get; set; }

        /// <summary>
        /// Name of the request associated with the application.
        /// </summary>
        public string RequestName { get; init; }

        /// <summary>
        /// Current status of the application.
        /// </summary>
        public string ApplicationStatus { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationStatusDTO"/> class with specified application ID, request name, and application status.
        /// </summary>
        /// <param name="applicationId">The unique identifier for the application.</param>
        /// <param name="identityUserId">The unique identifier for the application.</param>
        /// <param name="employeeId">The unique identifier for the application.</param>
        /// <param name="requestName">The name of the request associated with the application.</param>
        /// <param name="applicationStatus">The current status of the application represented by an <see cref="ApplicationStatusEnum"/> value.</param>
        public ApplicationStatusDTO(int applicationId, string? identityUserId, string? employeeId, string requestName, string applicationStatus)
        {
            ApplicationId = applicationId;
            IdentityUserId = identityUserId;
            EmployeeId = employeeId;
            RequestName = requestName;
            ApplicationStatus = applicationStatus;
        }
    }

}
