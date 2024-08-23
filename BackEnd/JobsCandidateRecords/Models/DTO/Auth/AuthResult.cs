namespace JobsCandidateRecords.Models.DTO.Auth
{
    /// <summary>
    /// Represents the result of an authentication operation, including tokens and potential errors.
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Gets or sets the authentication token issued upon successful authentication.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the refresh token used to obtain new authentication tokens.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authentication operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a list of error messages that occurred during the authentication process.
        /// </summary>
        public List<string>? Errors { get; set; }
    }

}
