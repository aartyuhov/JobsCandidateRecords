using System.ComponentModel.DataAnnotations;
namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for updating an existing company's information.
    /// </summary>
    public class UpdateCompanyDTO
    {
        /// <summary>
        /// The unique identifier of the company.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The updated name of the company.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The updated description of the company.
        /// </summary>
        /// <remarks>
        /// The maximum length for the description is 255 characters. This field is optional.
        /// </remarks>
        [MaxLength(255)]
        public string? Description { get; set; }
    }

}