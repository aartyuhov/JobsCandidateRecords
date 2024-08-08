namespace JobsCandidateRecords.Models.Input
{
    /// <summary>
    /// Model representing user registration information.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// The username of the user registering.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the user registering.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password for the new user account.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Confirmation of the password to ensure it matches the initial password.
        /// </summary>
        /// <remarks>
        /// This field is required and must match the <see cref="Password"/>.
        /// </remarks>
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}
