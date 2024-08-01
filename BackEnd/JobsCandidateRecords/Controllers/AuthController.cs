using JobsCandidateRecords.Models.Input;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

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
        private readonly IEmailSender _emailSender;

        public AuthController(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager,
                          IConfiguration configuration,
                          ILogger<LoginModel> logger, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
            _emailSender = emailSender;
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
            _logger.LogInformation("User logged out.");
            return Ok("User logged out.");
        }
        /// <summary>
        /// ForgotPassword
        /// </summary>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var resetLink = Url.Action("ResetPassword", "Auth", new { token = token, email = model.Email }, Request.Scheme);
            // Send email with the reset link
            await _emailSender.SendEmailAsync(model.Email, "Password Reset", $"Please reset your password by clicking here: <a href='{HtmlEncoder.Default.Encode(resetLink)}'>link</a>");

            return Ok("Password reset link has been sent.");
        }
        /// <summary>
        /// ResetPassword
        /// </summary>
        [HttpGet]
        public IActionResult ResetPassword(string Token, string Email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (Token == null || Email == null)
            {

                return BadRequest("The Link is Expired or Invalid");
            }
            else
            {
                ResetPasswordRequest model = new ResetPasswordRequest();
                model.Token = Token;
                model.Email = Email;
                //var data = new { Token, Email };
                //return Ok(data);
                return Ok(model);
            }
        }
        /// <summary>
        /// ResetPassword
        /// </summary>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
        {



            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            model.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Password has been reset successfully.");
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }

    }
}
