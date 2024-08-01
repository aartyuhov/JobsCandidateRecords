using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models.DTO;
using JobsCandidateRecords.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    /// /// <param name="userManager">The user manager to be used.</param>
    /// <param name="userStore">The user store to be used.</param>
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Requires "Admin" role for access
    [ApiController]
    public class UsersController(UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore) : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        //private readonly IUserStore<IdentityUser> _userStore = userStore;

        /// <summary>
        /// GetAllUsers. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users;
            return Ok(users);
        }
        /// <summary>
        /// GetUserRoleById. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpGet]
        [Route("{userId}")]
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
        /// GetAllUsersWithRoles. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpGet]
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
        /// GetUserRoleByEmail. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpGet]
        [Route("{userEmail}")]
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
        /// GetUserById. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpGet]
        [Route("{userId}")]
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
        /// GetUserByEmail. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpGet("{userEmail}")]
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
        /// GetUserByName. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpGet("{userName}")]
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
        /// CreateUser. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
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
        /// UpdateUserById. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserById(string userId, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
            }
            if (!string.IsNullOrEmpty(model.Username))
            {
                user.UserName = model.Username;
            }

            if (!string.IsNullOrEmpty(model.NewPassword) && string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("Old password is empty");
            }

            if (string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("New password is empty");
            }


            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.OldPassword))
            {
                var comparePasswords = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);
                if (comparePasswords != PasswordVerificationResult.Success)
                {
                    return BadRequest("Old password is wrong");
                }

                if (model.NewPassword == model.OldPassword)
                {
                    return BadRequest("Old password is the same with new password");
                }

                var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                user.PasswordHash = hashedNewPassword;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            return BadRequest(result.Errors);
        }
        /// <summary>
        /// UpdateUserByEmail. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpPut("{userEmail}")]
        public async Task<IActionResult> UpdateUserByEmail(string userEmail, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
            }
            if (!string.IsNullOrEmpty(model.Username))
            {
                user.UserName = model.Username;
            }

            if (!string.IsNullOrEmpty(model.NewPassword) && string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("Old password is empty");
            }

            if (string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("New password is empty");
            }


            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.OldPassword))
            {
                var comparePasswords = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);
                if (comparePasswords != PasswordVerificationResult.Success)
                {
                    return BadRequest("Old password is wrong");
                }

                if (model.NewPassword == model.OldPassword)
                {
                    return BadRequest("Old password is the same with new password");
                }

                var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                user.PasswordHash = hashedNewPassword;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// UpdateLoggedUser.Permission - Admin, User.
        /// </summary>
        [Authorize(Roles = "Admin, User")]
        [HttpPut]
        public async Task<IActionResult> UpdateLoggedUser([FromBody] UpdateUserModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                user.Email = model.Email;
            }
            if (!string.IsNullOrEmpty(model.Username))
            {
                user.UserName = model.Username;
            }

            if (!string.IsNullOrEmpty(model.NewPassword) && string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("Old password is empty");
            }

            if (string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("New password is empty");
            }


            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.OldPassword))
            {
                var comparePasswords = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);
                if (comparePasswords != PasswordVerificationResult.Success)
                {
                    return BadRequest("Old password is wrong");
                }

                if (model.NewPassword == model.OldPassword)
                {
                    return BadRequest("Old password is the same with new password");
                }

                var hashedNewPassword = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                user.PasswordHash = hashedNewPassword;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("User updated successfully");
            }
            return BadRequest(result.Errors);
        }
        /// <summary>
        /// GetLoggedUserWithRole. Permission - Admin, User.
        /// </summary>
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> GetLoggedUserWithRole()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            var usersDto = new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };

            if (usersDto == null)
            {
                return NotFound("User with roles wasn't found");
            }

            return Ok(usersDto);
        }

        /// <summary>
        /// GetLoggedUserWithRoleEmployee. Permission - Admin, User.
        /// </summary>
        [Authorize(Roles = "Admin, User")]
        [HttpGet("get-tuple")]
        public async Task<IActionResult> GetLoggedUserWithRoleEmployee()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var usersDto = new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,

                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };

            if (usersDto == null)
            {
                return NotFound("User with roles wasn't found");
            }

            var employeeDto = await _context.Employees.Where(e => e.IdentityUserId == user.Id).Select(e => new EmployeeDTO
            (
                e.Id,
                e.FirstName,
                e.LastName,
                e.DateOfBirth,
                e.Gender,
                e.Email,
                e.PhoneNumber,
                e.Address,
                e.HireDate,
                _context.Positions.Where(p => p.Id == e.PositionId).Select(p => p.Title).FirstOrDefault().ToString(),
                e.AvatarUrl
            )).FirstOrDefaultAsync();

            if (employeeDto == null)
            {
                return NotFound("Employee wasn't found");
            }

            return Ok(new { usersDto, employeeDto });

            //return Ok((usersDto, employeeDto));
        }

        /// <summary>
        /// DeleteUserById. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpDelete("{userId}")]
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
        /// DeleteUserByEmail. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpDelete("{userEmail}")]
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

        /// <summary>
        /// AssignEmployee. Permission - Admin.
        /// </summary>
        [Authorize(Roles = "Admin")] // Requires "Admin" role for access
        [HttpPost("{userId}/assign/{employeeId}")]
        public async Task<IActionResult> AssignEmployee(string userId, int employeeId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }
            var employee = await _context.Employees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound("Employee wasn't found");
            }

            employee.IdentityUserId = user.Id;

            await _context.SaveChangesAsync();

            return Ok($"User '{user.UserName}' assigned to emplyee '{employee.FirstName} {employee.LastName}' successfully");

        }
    }
}
