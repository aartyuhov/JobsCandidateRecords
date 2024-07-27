using JobsCandidateRecords.Models.Input;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobsCandidateRecords.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginModel> _logger;

        public AuthController(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager,
                          IConfiguration configuration,
                          ILogger<LoginModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }
        /// <summary>
        /// Register.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                return Ok("User created successfully");
            }

            return BadRequest(result.Errors);
        }
        /// <summary>
        /// Login.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var emailresult = _userManager.FindByEmailAsync(model.Email).Result;
            if (emailresult != null)
            {
                var result = await _signInManager.PasswordSignInAsync(emailresult, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return Ok("User logged in.");
                };
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return Ok("User account locked out.");
                };
            }
            return Unauthorized();
        }
        /// <summary>
        /// Logout.
        /// </summary>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            //Clear cart

            if (Request.Cookies.ContainsKey("CookieCart"))
            {
                Response.Cookies.Append("CookieCart", "");

            }
            _logger.LogInformation("User logged out.");
            return Ok("User logged out.");
        }

    }
}
