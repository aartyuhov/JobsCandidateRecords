using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models.Input;
using JobsCandidateRecords.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing users and their roles.
    /// Provides API for getting, creating, updating and deleting users, as well as managing roles and relationships with employees.    
    /// </summary>
    /// /// <remarks>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </remarks>
    /// <param name="userService">User management service.</param>
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Requires "Admin" role for access
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>An action result containing the list of users.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a list of all users with their roles.
        /// </summary>
        /// <returns>An action result containing the list of users with roles, or a 404 status if no users are found.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("all-with-roles")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            var usersDto = await _userService.GetAllUsersWithRolesAsync();
            if (usersDto == null || !usersDto.Any())
            {
                return NotFound("Users with roles weren't found");
            }

            return Ok(usersDto);
        }

        /// <summary>
        /// Retrieves the roles of a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose roles are to be retrieved.</param>
        /// <returns>An action result containing the roles of the user, or a 404 status if the user is not found.</returns>

        [Authorize(Roles = "Admin")]
        [HttpGet("roles/{userId}")]
        public async Task<IActionResult> GetUserRoleById(string userId)
        {
            var userDto = await _userService.GetUserByIdAsync(userId);
            if (userDto == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(userDto.Roles);
        }

        /// <summary>
        /// Retrieves the roles of a user by their email address.
        /// </summary>
        /// <param name="userEmail">The email address of the user whose roles are to be retrieved.</param>
        /// <returns>An action result containing the roles of the user, or a 404 status if the user is not found.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("roles/email/{userEmail}")]
        public async Task<IActionResult> GetUserRoleByEmail(string userEmail)
        {
            var userDto = await _userService.GetUserByEmailAsync(userEmail);
            if (userDto == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(userDto.Roles);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>An action result containing the user details, or a 404 status if the user is not found.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var userDto = await _userService.GetUserByIdAsync(userId);
            if (userDto == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="userEmail">The email address of the user to retrieve.</param>
        /// <returns>An action result containing the user details, or a 404 status if the user is not found.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("user/email/{userEmail}")]
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            var userDto = await _userService.GetUserByEmailAsync(userEmail);
            if (userDto == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to retrieve.</param>
        /// <returns>An action result containing the user details, or a 404 status if the user is not found.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("user/name/{userName}")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var userDto = await _userService.GetUserByNameAsync(userName);
            if (userDto == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="model">The model containing user creation data.</param>
        /// <returns>An action result indicating whether the user was created successfully or not.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            var result = await _userService.CreateUserAsync(model);
            if (result.Succeeded)
            {
                return Ok("User created successfully");
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Updates a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to update.</param>
        /// <param name="model">The model containing updated user data.</param>
        /// <returns>An action result indicating whether the user was updated successfully or not.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserById(string userId, [FromBody] UpdateUserModel model)
        {
            var result = await _userService.UpdateUserByIdAsync(userId, model);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        /// <returns>An action result indicating whether the user was deleted successfully or not.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserById(string userId)
        {
            var result = await _userService.DeleteUserByIdAsync(userId);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully");
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Assigns an employee to a user.
        /// </summary>
        /// <param name="userId">The ID of the user to assign the employee to.</param>
        /// <param name="employeeId">The ID of the employee to assign.</param>
        /// <returns>An action result indicating whether the assignment was successful or not.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("{userId}/assign/{employeeId}")]
        public async Task<IActionResult> AssignEmployee(string userId, int employeeId)
        {
            var result = await _userService.AssignEmployeeAsync(userId, employeeId);
            if (result.Succeeded)
            {
                return Ok("User assigned to employee successfully");
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Updates the details of the currently logged-in user.
        /// </summary>
        /// <param name="model">The model containing updated user data.</param>
        /// <returns>An action result indicating whether the update was successful or not.</returns>
        [HttpPut("update-logged")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateLoggedUser([FromBody] UpdateUserModel model)
        {
            var userId = _userService.GetUserId(User);
            if (userId == null)
            {
                return NotFound("User with roles wasn't found");
            }

            var result = await _userService.UpdateUserByIdAsync(userId, model);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Retrieves details of the currently logged-in user with their roles.
        /// </summary>
        /// <returns>An action result containing the details of the logged-in user, or a 404 status if the user is not found.</returns>
        [HttpGet("logged-with-role")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetLoggedUserWithRole()
        {
            var userId = _userService.GetUserId(User);
            if (userId == null)
            {
                return NotFound("User with roles wasn't found");
            }

            var userDto = await _userService.GetLoggedUserWithRoleAsync(userId);
            if (userDto == null)
            {
                return NotFound("User with roles wasn't found");
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Retrieves details of the currently logged-in user with their roles and associated employee details.
        /// </summary>
        /// <returns>An action result containing the details of the logged-in user with their roles and employee information, or a 404 status if the information is not found.</returns>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("get-tuple")]
        public async Task<IActionResult> GetLoggedUserWithRoleEmployee()
        {
            var userId = _userService.GetUserId(User);
            if (userId == null)
            {
                return NotFound("User with roles wasn't found");
            }

            var result = await _userService.GetLoggedUserWithRoleEmployeeAsync(userId);

            if (result == null)
            {
                return NotFound("User or employee information wasn't found");
            }

            return Ok(result);
        }
    }
}
