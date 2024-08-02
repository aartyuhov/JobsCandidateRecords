namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for a department within the organization.
    /// </summary>
    public class DepartmentDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the department.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets or sets the description of the department.
        /// </summary>
        /// <value>
        /// A string providing details about the department, or <c>null</c> if not specified.
        /// </value>
        public string? Description { get; init; }

        /// <summary>
        /// Gets or sets the identifier of the company to which the department belongs.
        /// </summary>
        public int CompanyId { get; init; }

        /// <summary>
        /// Gets or sets the name of the company to which the department belongs.
        /// </summary>
        /// <value>
        /// A string representing the name of the company, or <c>null</c> if not specified.
        /// </value>
        public string? CompanyName { get; init; }

        /// <summary>
        /// Constructor for DepartmentDTO.
        /// </summary>
        /// <param name="id">Unique identifier of the department.</param>
        /// <param name="name">Name of the department.</param>
        /// <param name="description">Description of the department.</param>
        /// <param name="companyId">Identifier of the company to which the department belongs.</param>
        /// <param name="companyName">Name of the company to which the department belongs.</param>
        public DepartmentDTO(
            int id,
            string name,
            string? description,
            int companyId,
            string? companyName)
        {
            Id = id;
            Name = name;
            Description = description;
            CompanyId = companyId;
            CompanyName = companyName;
        }
    }

}