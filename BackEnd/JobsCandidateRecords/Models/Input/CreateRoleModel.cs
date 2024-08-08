namespace JobsCandidateRecords.Models.Input
{
    /// <summary>
    /// Model representing the data required to create a new role.
    /// </summary>
    public class CreateRoleModel
    {
        /// <summary>
        /// The name of the role to be created.
        /// </summary>
        /// <remarks>
        /// This field is required and represents the name of the new role.
        /// </remarks>
        public string RoleName { get; set; } = string.Empty;
    }

}
