namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for employee request.
    /// </summary>
    public record RequestForEmployeeDTO
    {
        /// <summary>
        /// Request identifier.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Request name.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Date of publication of the request.
        /// </summary>
        public DateTime? PublicationDate { get; init; }

        /// <summary>
        /// Number of employees required.
        /// </summary>
        public int NumberEmployessRequired { get; init; }

        /// <summary>
        /// Request description.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Position identifier.
        /// </summary>
        public int? PositionId { get; init; }

        /// <summary>
        /// Position name.
        /// </summary>
        public string? PositionName { get; init; }

        /// <summary>
        /// Requested employee identifier.
        /// </summary>
        public int RequestedEmployeeId { get; init; }

        /// <summary>
        /// Full name of the requested employee.
        /// </summary>
        public string? RequestedEmployeeFullName { get; init; }

        /// <summary>
        /// List of applications.
        /// </summary>
        public List<ApplicationDTO>? Applications { get; init; }

        /// <summary>
        /// Constructor for RequestForEmployeeDTO.
        /// </summary>
        /// <param name="id">Request identifier.</param>
        /// <param name="name">Request name.</param>
        /// <param name="publicationDate">Date of publication of the request.</param>
        /// <param name="numberEmployessRequired">Number of employees required.</param>
        /// <param name="description">Request description.</param>
        /// <param name="positionId">Position identifier.</param>
        /// <param name="positionName">Position name.</param>
        /// <param name="requestedEmployeeId">Requested employee identifier.</param>
        /// <param name="requestedEmployeeFullName">Full name of the requested employee.</param>
        /// <param name="applications">List of applications.</param>
        public RequestForEmployeeDTO(
            int id,
            string name,
            DateTime? publicationDate,
            int numberEmployessRequired,
            string description,
            int? positionId,
            string? positionName,
            int requestedEmployeeId,
            string? requestedEmployeeFullName,
            List<ApplicationDTO>? applications)
        {
            Id = id;
            Name = name;
            PublicationDate = publicationDate;
            NumberEmployessRequired = numberEmployessRequired;
            Description = description;
            PositionId = positionId;
            PositionName = positionName;
            RequestedEmployeeId = requestedEmployeeId;
            RequestedEmployeeFullName = requestedEmployeeFullName;
            Applications = applications;
        }
    }
}
