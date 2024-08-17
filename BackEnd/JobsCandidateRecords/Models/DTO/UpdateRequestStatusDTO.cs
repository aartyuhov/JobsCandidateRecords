using JobsCandidateRecords.Enums;

namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// Data Transfer Object for updating the status of multiple requests.
    /// </summary>
    public class UpdateRequestStatusDTO
    {
        /// <summary>
        /// Gets or sets the list of requests IDs to update.
        /// </summary>
        public List<int> RequestsIds { get; set; } = [];

        /// <summary>
        /// Gets or sets the new status to be applied to the requests.
        /// </summary>
        public RequestForEmployeeStatusEnum NewStatus { get; set; }
    }
}
