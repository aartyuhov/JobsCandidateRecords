namespace JobsCandidateRecords.Models.DTO
{
    /// <summary>
    /// Data transfer object representing a user.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        /// <remarks>
        /// This field is required and represents the user's ID.
        /// </remarks>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The username of the user.
        /// </summary>
        /// <remarks>
        /// This field is required and represents the user's chosen username.
        /// </remarks>
        public string? Username { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        /// <remarks>
        /// This field is required and must be a valid email address.
        /// </remarks>
        public string? Email { get; set; } = string.Empty;

        /// <summary>
        /// A list of roles assigned to the user.
        /// </summary>
        /// <remarks>
        /// This field contains the roles associated with the user. It may be empty if the user has no roles assigned.
        /// </remarks>
        public IList<string> Roles { get; set; } = [];
    }

}
