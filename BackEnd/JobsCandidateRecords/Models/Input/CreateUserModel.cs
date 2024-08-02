using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.Input
{
    /// <summary>
    /// Model representing the data required to create a new user account.
    /// </summary>
    public class CreateUserModel
    {
        /// <summary>
        /// The username of the new user.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the new user.
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
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Confirmation of the password to ensure it matches the entered password.
        /// </summary>
        /// <remarks>
        /// This field must match the <see cref="Password"/>. If the passwords do not match, an error will be raised.
        /// </remarks>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

}
