using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.Input
{
    /// <summary>
    /// Model representing the data required to update a user's account information.
    /// </summary>
    public class UpdateUserModel
    {
        /// <summary>
        /// The updated email address of the user.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The updated username of the user.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The current password of the user for verification.
        /// </summary>
        /// <remarks>
        /// This field is required if the user wants to update their password.
        /// </remarks>
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;

        /// <summary>
        /// The new password for the user's account.
        /// </summary>
        /// <remarks>
        /// This field is required if the user wants to update their password.
        /// </remarks>
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
