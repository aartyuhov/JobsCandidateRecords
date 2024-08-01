namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for application information.
    /// </summary>
    public record ApplicationDTO
    {
        /// <summary>
        /// Application identifier.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Candidate associated with the application.
        /// </summary>
        public CandidateDTO Candidate { get; init; }

        /// <summary>
        /// Identifier of the employee who created the application.
        /// </summary>
        public int? EmployeeWhoCreatedId { get; init; }

        /// <summary>
        /// Date when the application was created.
        /// </summary>
        public DateTime? CreationDate { get; init; }

        /// <summary>
        /// Details of the application.
        /// </summary>
        public string Details { get; init; }

        /// <summary>
        /// Status of the application.
        /// </summary>
        public string ApplicationStatus { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDTO"/> record.
        /// </summary>
        /// <param name="id">Application identifier.</param>
        /// <param name="candidate">Candidate associated with the application.</param>
        /// <param name="employeeWhoCreatedId">Identifier of the employee who created the application.</param>
        /// <param name="creationDate">Date when the application was created.</param>
        /// <param name="details">Details of the application.</param>
        /// <param name="applicationStatus">Status of the application.</param>
        public ApplicationDTO(
            int id,
            CandidateDTO candidate,
            int? employeeWhoCreatedId,
            DateTime? creationDate,
            string details,
            string applicationStatus)
        {
            Id = id;
            Candidate = candidate;
            EmployeeWhoCreatedId = employeeWhoCreatedId;
            CreationDate = creationDate;
            Details = details;
            ApplicationStatus = applicationStatus;
        }
    }

}
