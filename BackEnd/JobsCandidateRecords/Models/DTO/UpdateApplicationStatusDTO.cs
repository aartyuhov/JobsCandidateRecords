using JobsCandidateRecords.Enums;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// Data Transfer Object for updating the status of multiple applications.
    /// </summary>
    public class UpdateApplicationStatusDTO
    {
        /// <summary>
        /// Gets or sets the list of application IDs to update.
        /// </summary>
        public List<int> ApplicationIds { get; set; } = [];

        /// <summary>
        /// Gets or sets the new status to be applied to the applications.
        /// </summary>
        public ApplicationStatusEnum NewStatus { get; set; }
    }
}
