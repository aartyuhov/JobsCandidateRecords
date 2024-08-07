namespace JobsCandidateRecords.Models.Input
{
    /// <summary>
    /// Model representing the data required to request a password reset.
    /// </summary>
    public class ForgotPasswordRequest
    {
        /// <summary>
        /// The email address of the user requesting a password reset.
        /// </summary>
        /// <remarks>
        /// This field is required and must be a valid email address.
        /// </remarks>
        public string Email { get; set; } = string.Empty;
    }
}
