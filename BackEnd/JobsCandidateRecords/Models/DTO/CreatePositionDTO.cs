using System;
using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// DTO for creating a position within a department.
    /// </summary>
    public class CreatePositionDTO
    {
        /// <summary>
        /// The title of the position.
        /// </summary>
        /// <remarks>
        /// This field is required and the maximum length is 50 characters.
        /// </remarks>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The responsibilities associated with the position.
        /// </summary>
        /// <remarks>
        /// The maximum length for the responsibilities is 255 characters. This field is optional.
        /// </remarks>
        [MaxLength(255)]
        public string? Responsibilities { get; set; }

        /// <summary>
        /// The ID of the department to which the position belongs.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        public int DepartmentId { get; set; }
    }

}