namespace JobsCandidateRecords.Models.Input
{
    /// <summary>
    /// Model representing the data required for user login.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// The email address of the user attempting to log in.
        /// </summary>
        /// <remarks>
        /// This field is required and must be a valid email address.
        /// </remarks>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password for the user account.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the user wants to be remembered on this device.
        /// </summary>
        /// <remarks>
        /// If set to true, the user's authentication session will persist across browser sessions.
        /// </remarks>
        public bool RememberMe { get; set; }
    }

}
