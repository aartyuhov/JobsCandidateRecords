namespace JobsCandidateRecords.Models.DTO
{
    namespace JobsCandidateRecords.Models.DTO
    {
        /// <summary>
        /// DTO for updating application information.
        /// </summary>
        public record UpdateApplicationDTO
        {
            /// <summary>
            /// Application identifier.
            /// </summary>
            public int Id { get; init; }

            /// <summary>
            /// Identifier of the candidate associated with the application.
            /// </summary>
            public int? CandidateId { get; init; }

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
            /// Initializes a new instance of the <see cref="UpdateApplicationDTO"/> record.
            /// </summary>
            /// <param name="id">Application identifier.</param>
            /// <param name="candidateId">Identifier of the candidate associated with the application.</param>
            /// <param name="employeeWhoCreatedId">Identifier of the employee who created the application.</param>
            /// <param name="creationDate">Date when the application was created.</param>
            /// <param name="details">Details of the application.</param>
            /// <param name="applicationStatus">Status of the application.</param>
            public UpdateApplicationDTO(
                int id,
                int? candidateId,
                int? employeeWhoCreatedId,
                DateTime? creationDate,
                string details,
                string applicationStatus)
            {
                Id = id;
                CandidateId = candidateId;
                EmployeeWhoCreatedId = employeeWhoCreatedId;
                CreationDate = creationDate;
                Details = details;
                ApplicationStatus = applicationStatus;
            }
        }
    }
}
