using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.DTO.Auth.Request
{
    /// <summary>
    /// Represents the data transfer object (DTO) used for requesting a new token using a refresh token.
    /// </summary>
    public class TokenRequestDTO
    {
        /// <summary>
        /// Gets or sets the refresh token used to request a new access token.
        /// </summary>
        /// <remarks>
        /// The refresh token is used to obtain a new access token when the current access token expires.
        /// </remarks>
        [Required(ErrorMessage = "Refresh token is required.")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
