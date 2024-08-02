using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models.Input;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Service for managing user-related operations, including retrieval, creation, update, and deletion of users.
    /// Implements the <see cref="IUserService"/> interface.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </remarks>
    /// <param name="userManager">The user manager instance used for user management operations.</param>
    /// <param name="context">The application database context instance.</param>
    public class UserService(UserManager<IdentityUser> userManager, ApplicationDbContext context) : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        /// Retrieves all users from the system.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The result contains an enumerable collection of <see cref="IdentityUser"/> objects.</returns>
        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        /// <summary>
        /// Retrieves the unique identifier of the user from the provided <see cref="ClaimsPrincipal"/> object.
        /// </summary>
        /// <param name="user">The <see cref="ClaimsPrincipal"/> representing the current user or context.</param>
        /// <returns>The unique identifier of the user as a <see cref="string"/>, or <c>null</c> if the user ID cannot be found.</returns>
        public string? GetUserId(ClaimsPrincipal user)
        {
            return _userManager.GetUserId(user);
        }


        /// <summary>
        /// Retrieves all users along with their roles.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a result containing an enumerable of <see cref="UserDTO"/>.</returns>
        public async Task<IEnumerable<UserDTO>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return await Task.WhenAll(users.Select(async u => new UserDTO
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Roles = await _userManager.GetRolesAsync(u)
            }));
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        public async Task<UserDTO?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="userEmail">The email address of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        public async Task<UserDTO?> GetUserByEmailAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        public async Task<UserDTO?> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        /// <summary>
        /// Creates a new user based on the provided model.
        /// </summary>
        /// <param name="model">The model containing the details of the user to be created.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        public async Task<IdentityResult> CreateUserAsync(CreateUserModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        /// <summary>
        /// Updates an existing user based on their unique identifier and the provided model.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be updated.</param>
        /// <param name="model">The model containing the updated user details.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        public async Task<IdentityResult> UpdateUserByIdAsync(string userId, UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            if (!string.IsNullOrEmpty(model.Email)) user.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Username)) user.UserName = model.Username;

            if (!string.IsNullOrEmpty(model.NewPassword) || !string.IsNullOrEmpty(model.OldPassword))
            {
                if (string.IsNullOrEmpty(model.OldPassword))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Old password is required" });
                }

                if (string.IsNullOrEmpty(model.NewPassword))
                {
                    return IdentityResult.Failed(new IdentityError { Description = "New password is required" });
                }

                if (user.PasswordHash == null)
                {
                    // Handle the case where PasswordHash is null
                    throw new InvalidOperationException("User does not have a password hash.");
                }

                var passwordResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);
                if (passwordResult != PasswordVerificationResult.Success)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Old password is incorrect" });
                }

                if (model.NewPassword == model.OldPassword)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "New password must be different from old password" });
                }

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            }

            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Deletes a user based on their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to be deleted.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        public async Task<IdentityResult> DeleteUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            return await _userManager.DeleteAsync(user);
        }

        /// <summary>
        /// Retrieves the logged-in user along with their roles.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing a <see cref="UserDTO"/>.</returns>
        public async Task<UserDTO?> GetLoggedUserWithRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        /// <summary>
        /// Retrieves the logged-in user along with their roles and employee details if they are associated with an employee record.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation. The result contains an anonymous object with the following properties:
        /// <list type="bullet">
        /// <item><description><see cref="UserDTO"/> containing user details and roles.</description></item>
        /// <item><description><see cref="EmployeeDTO"/> containing employee details if the user is associated with an employee record; otherwise, <c>null</c>.</description></item>
        /// </list>
        /// If the user is not found, the result will be <c>null</c>.</returns>
        public async Task<object?> GetLoggedUserWithRoleEmployeeAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var employeeDto = await _context.Employees
                .Where(e => e.IdentityUserId == userId)
                .Select(e => new EmployeeDTO(
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.DateOfBirth,
                    e.Gender,
                    e.Email,
                    e.PhoneNumber,
                    e.Address,
                    e.HireDate,
                    _context.Positions.Where(p => p.Id == e.PositionId).Select(p => p.Title).FirstOrDefault(),
                    e.AvatarUrl
                ))
                .FirstOrDefaultAsync();

            return new
            {
                usersDto = new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = roles
                },
                employeeDto
            };
        }

        /// <summary>
        /// Assigns an employee ID to a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to whom the employee ID will be assigned.</param>
        /// <param name="employeeId">The employee ID to be assigned to the user.</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the <see cref="IdentityResult"/> of the operation.</returns>
        public async Task<IdentityResult> AssignEmployeeAsync(string userId, int employeeId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Employee not found" });
            }

            employee.IdentityUserId = user.Id;
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }
    }
}
