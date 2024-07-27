using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Requires "Admin" role for access
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;

        public UsersController(UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
        }

        /// <summary>
        /// GetAllUsers.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }
        /// <summary>
        /// GetUserRoleById.
        /// </summary>
        [HttpGet]
        [Route("UserRoles/UsersId/{userId}")]
        public async Task<IActionResult> GetUserRoleById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't find");
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                return NotFound("Role wasn't find");
            }

            return Ok(roles);
        }
        /// <summary>
        /// GetAllUsersWithRoles.
        /// </summary>
        [HttpGet]
        [Route("UserRoles/")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDto = users.Select(async u => new UserDTO
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Roles = await _userManager.GetRolesAsync(u)
            });

            if (usersDto == null)
            {
                return NotFound("Users with roles weren't found");
            }

            return Ok(usersDto);
        }
        /// <summary>
        /// GetUserRoleByEmail.
        /// </summary>
        [HttpGet]
        [Route("UserRoles/UsersEmail/{userEmail}")]
        public async Task<IActionResult> GetUserRoleByEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                return NotFound("Role wasn't found");
            }

            return Ok(roles);
        }
        /// <summary>
        /// GetUserById.
        /// </summary>
        [HttpGet]
        [Route("UsersId/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(user);
        }
        /// <summary>
        /// GetUserByEmail.
        /// </summary>
        [HttpGet("UsersEmail/{userEmail}")]
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(user);
        }
        /// <summary>
        /// GetUserByName.
        /// </summary>
        [HttpGet("UsersName/{userName}")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            return Ok(user);
        }
        /// <summary>
        /// CreateUser.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok("User created successfully");
            }
            return BadRequest(result.Errors);

        }
        /// <summary>
        /// UpdateUserById.
        /// </summary>
        [HttpPut("UsersId/{userId}")]
        public async Task<IActionResult> UpdateUserById(string userId, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            user.Email = model.Email;
            user.UserName = model.Username;


            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            return BadRequest(result.Errors);
        }
        /// <summary>
        /// UpdateUserByEmail.
        /// </summary>
        [HttpPut("UsersEmail/{userEmail}")]
        public async Task<IActionResult> UpdateUserByEmail(string userEmail, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            user.Email = model.Email;
            user.UserName = model.Username;
            var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, model.Password);
            user.PasswordHash = hashedNewPassword;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// DeleteUserById.
        /// </summary>

        [HttpDelete("UsersId/{userId}")]
        public async Task<IActionResult> DeleteUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully");
            }
            return BadRequest(result.Errors);
        }
        /// <summary>
        /// DeleteUserByEmail.
        /// </summary>
        [HttpDelete("UsersEmail/{userEmail}")]
        public async Task<IActionResult> DeleteUserByEmail(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully");
            }
            return BadRequest(result.Errors);
        }
    }
}
