using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models.Input;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Interface for managing user-related operations, including retrieval, creation, update, and deletion of users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The result contains an enumerable collection of <see cref="IdentityUser"/> objects.</returns>
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();

        /// <summary>
        /// Retrieves the unique identifier of the user from the provided <see cref="ClaimsPrincipal"/> object.
        /// </summary>
        /// <param name="user">The <see cref="ClaimsPrincipal"/> representing the current user or context.</param>
        /// <returns>The unique identifier of the user as a <see cref="string"/>, or <c>null</c> if the user ID cannot be found.</returns>
        string? GetUserId(ClaimsPrincipal user);


        /// <summary>
        /// Retrieves all users along with their roles.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result containing an enumerable of <see cref="UserDTO"/>.</returns>
        Task<IEnumerable<UserDTO>> GetAllUsersWithRolesAsync();

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        Task<UserDTO?> GetUserByIdAsync(string userId);

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="userEmail">The email address of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        Task<UserDTO?> GetUserByEmailAsync(string userEmail);

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        Task<UserDTO?> GetUserByNameAsync(string userName);

        /// <summary>
        /// Creates a new user based on the provided model.
        /// </summary>
        /// <param name="model">The model containing the details of the user to be created.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> CreateUserAsync(CreateUserModel model);

        /// <summary>
        /// Updates an existing user based on their unique identifier and the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be updated.</param>
        /// <param name="model">The model containing the updated user details.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> UpdateUserByIdAsync(string userId, UpdateUserModel model);

        /// <summary>
        /// Deletes a user based on their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> DeleteUserByIdAsync(string userId);

        /// <summary>
        /// Retrieves the logged-in user along with their roles.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        Task<UserDTO?> GetLoggedUserWithRoleAsync(string userId);

        /// <summary>
        /// Retrieves the logged-in user along with their roles, specifically checking for the employee role.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The result contains an <see cref="object"/> that represents the logged-in user with their roles, or <c>null</c> if the user is not found.</returns>
        Task<object?> GetLoggedUserWithRoleEmployeeAsync(string userId);

        /// <summary>
        /// Assigns an employee ID to a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to whom the employee ID will be assigned.</param>
        /// <param name="employeeId">The employee ID to be assigned to the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        Task<IdentityResult> AssignEmployeeAsync(string userId, int employeeId);
    }
}
