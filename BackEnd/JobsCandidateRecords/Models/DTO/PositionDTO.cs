using System;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for a position within the organization.
    /// </summary>
    public class PositionDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the position.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the title of the position.
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Gets or sets the responsibilities associated with the position.
        /// </summary>
        /// <value>
        /// A string describing the responsibilities of the position, or <c>null</c> if not specified.
        /// </value>
        public string? Responsibilities { get; init; }

        /// <summary>
        /// Gets or sets the identifier of the department to which the position belongs.
        /// </summary>
        public int DepartmentId { get; init; }

        /// <summary>
        /// Gets or sets the name of the department to which the position belongs.
        /// </summary>
        /// <value>
        /// A string representing the name of the department, or <c>null</c> if not specified.
        /// </value>
        public string? DepartmentName { get; init; }

        /// <summary>
        /// Constructor for PositionDTO.
        /// </summary>
        /// <param name="id">Unique identifier of the position.</param>
        /// <param name="title">Title of the position.</param>
        /// <param name="responsibilities">Responsibilities associated with the position.</param>
        /// <param name="departmentId">Identifier of the department to which the position belongs.</param>
        /// <param name="departmentName">Name of the department to which the position belongs.</param>
        public PositionDTO(
            int id,
            string title,
            string? responsibilities,
            int departmentId,
            string? departmentName)
        {
            Id = id;
            Title = title;
            Responsibilities = responsibilities;
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }
    }

}