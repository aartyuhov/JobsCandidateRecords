using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for creating a company.
    /// </summary>
    public class CreateCompanyDTO
    {
        /// <summary>
        /// The name of the company.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A description of the company.
        /// </summary>
        /// <remarks>
        /// The maximum length for the description is 255 characters. This field is optional.
        /// </remarks>
        [MaxLength(255)]
        public string? Description { get; set; }
    }

}