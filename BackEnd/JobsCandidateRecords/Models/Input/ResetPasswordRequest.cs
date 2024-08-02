using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.Input
{
    /// <summary>
    /// Model representing the data required to reset a user's password.
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// The token used to authenticate the password reset request.
        /// </summary>
        /// <remarks>
        /// This field is required for verifying the reset request.
        /// </remarks>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the user requesting the password reset.
        /// </summary>
        /// <remarks>
        /// This field is required and must be a valid email address.
        /// </remarks>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The new password to be set for the user account.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// Confirmation of the new password to ensure it matches the entered new password.
        /// </summary>
        /// <remarks>
        /// This field must match the <see cref="NewPassword"/>. If the passwords do not match, an error will be raised.
        /// </remarks>
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}
