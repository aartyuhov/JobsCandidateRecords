using JobsCandidateRecords.Models.Input;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for managing roles and user roles.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Requires "Admin" role for access
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="roleManager">The role manager to be used.</param>
        /// <param name="userManager">The user manager to be used.</param>
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// GetAllRoles. Permission - Admin.
        /// </summary>
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }
        /// <summary>
        /// CreateRole. Permission - Admin.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleModel model)
        {
            var role = new IdentityRole(model.RoleName);

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok("Role created successfully");
            }

            return BadRequest(result.Errors);
        }
        /// <summary>
        /// DeleteRole. Permission - Admin.
        /// </summary>
        [HttpDelete("{roleName}")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound($"Role '{roleName}' not found");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok("Role deleted successfully");
            }

            return BadRequest(result.Errors);
        }
        /// <summary>
        /// AssignRole. Permission - Admin.
        /// </summary>
        [HttpPost("{userId}/assign/{roleName}")]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound($"Role '{roleName}' not found");
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok($"User '{user.UserName}' assigned to role '{roleName}' successfully");
            }

            return BadRequest(result.Errors);
        }
        /// <summary>
        /// RemoveRole. Permission - Admin.
        /// </summary>
        [HttpDelete("{userId}/remove/{roleName}")]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User wasn't found");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' removed from user '{user.UserName}' successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}
