namespace JobsCandidateRecords.Config
{
    /// <summary>
    /// Configuration settings for JWT (JSON Web Token) authentication.
    /// </summary>
    public class JwtConfig
    {
        /// <summary>
        /// Gets or sets the secret key used for signing and verifying JWT tokens.
        /// </summary>
        /// <remarks>
        /// This secret should be a strong, unique value that is kept confidential.
        /// </remarks>
        public string? Secret { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expiration time in minutes for the access token.
        /// </summary>
        /// <remarks>
        /// This value determines how long the access token remains valid after being issued.
        /// </remarks>
        public double AccessTokenExpirationMinutes { get; set; } = 1;

        /// <summary>
        /// Gets or sets the expiration time in months for the refresh token.
        /// </summary>
        /// <remarks>
        /// This value determines how long the refresh token remains valid before it needs to be renewed.
        /// </remarks>
        public int RefreshTokenExpirationMonths { get; set; } = 1;
    }

}
