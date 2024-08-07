using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for creating a department.
    /// </summary>
    public class CreateDepartmentDTO
    {
        /// <summary>
        /// The name of the department.
        /// </summary>
        /// <remarks>
        /// This field is required and the maximum length is 50 characters.
        /// </remarks>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A description of the department.
        /// </summary>
        /// <remarks>
        /// The maximum length for the description is 255 characters. This field is optional.
        /// </remarks>
        [MaxLength(255)]
        public string? Description { get; set; }

        /// <summary>
        /// The ID of the company to which the department belongs.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public int CompanyId { get; set; }
    }

}