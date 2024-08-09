namespace JobsCandidateRecords.Models.DTO.Auth.Response
{
    /// <summary>
    /// Represents the response data transfer object (DTO) for refresh token operations.
    /// </summary>
    public class RefreshTokenResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user associated with the refresh token.
        /// </summary>
        public string IdentityUserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the refresh token operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a list of error messages related to the refresh token operation.
        /// </summary>
        /// <remarks>
        /// This list will contain any error messages if the operation failed.
        /// </remarks>
        public List<string> Errors { get; set; } = [];
    }

}
