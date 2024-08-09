using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models.DTO.Auth;
using JobsCandidateRecords.Models.DTO.Auth.Request;
using JobsCandidateRecords.Models.Input;
using JobsCandidateRecords.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace JobsCandidateRecords.Controllers
{
    /// <summary>
    /// Controller for handling user authentication and account management.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </remarks>
    /// <param name="userManager">The user manager service for managing user accounts.</param>
    /// <param name="logger">The logger service for logging information and errors.</param>
    /// <param name="emailSender">The email sender service for sending email messages.</param>
    /// <param name="jwtService">The jwt service for managing tokins.</param>
    /// <param name="context">The context service for communicate with DB.</param>

    /*/// <param name="signInManager">The sign-in manager service for user authentication.</param>*/
    /*/// <param name="configuration">The configuration service for retrieving app settings.</param>*/
    /*/// <param name="userService">The user service for managing users.</param>*/
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(UserManager<IdentityUser> userManager,
                          /*SignInManager<IdentityUser> signInManager,*/
                          /*IConfiguration configuration,*/
                          ILogger<AuthController> logger,
                          IEmailSender emailSender,
                          IJwtService jwtService,
                          /*IUserService userService,*/
                          ApplicationDbContext context) : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        //private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        //private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IJwtService _jwtService = jwtService;
        //private readonly IUserService _userService = userService;
        private readonly ApplicationDbContext _context = context;


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration model containing user details.</param>
        /// <returns>An IActionResult indicating the outcome of the registration process.</returns>
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
                AuthResult authResult = await _jwtService.GenerateToken(user);
                //return Ok("User created successfully.");
                return Ok(authResult);
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="model">The login model containing user credentials.</param>
        /// <returns>An IActionResult indicating the outcome of the login process.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                //var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                //if (result.Succeeded)
                //{
                //    _logger.LogInformation("User logged in.");
                //    return Ok("User logged in.");
                //}

                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return Ok("User account locked out.");
                //}

                bool isUserCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
                if (isUserCorrect)
                {
                    AuthResult authResult = await _jwtService.GenerateToken(user);
                    //return a token
                    return Ok(authResult);
                }
            }

            return Unauthorized();
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <returns>An IActionResult indicating the outcome of the logout process.</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {

            var userId = HttpContext.User.FindFirstValue("Id");

            //var userId = _userService.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            //await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");

            var refreshTokens = await _context.RefreshTokens.Where(rt => rt.IdentityUserId == userId).ToListAsync();

            if (refreshTokens != null)
            {
                foreach (var rt in refreshTokens)
                {
                    rt.IsRevoked = true;
                }

                _logger.LogInformation("Refresh tokens were revoked");
                await _context.SaveChangesAsync();

            }

            return NoContent();
        }

        /// <summary>
        /// Initiates a password reset process by sending a reset link to the user.
        /// </summary>
        /// <param name="model">The request model containing the user's email.</param>
        /// <returns>An IActionResult indicating the outcome of the password reset request.</returns>
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
            var resetLink = Url.Action("ResetPassword", "Auth", new { token, email = model.Email }, Request.Scheme);


            // Check if resetLink is null before encoding
            var encodedResetLink = resetLink != null ? HtmlEncoder.Default.Encode(resetLink) : string.Empty;

            // Send email with the reset link
            await _emailSender.SendEmailAsync(model.Email, "Password Reset",
                $"Please reset your password by clicking here: <a href='{encodedResetLink}'>link</a>");

            return Ok("Password reset link has been sent.");
        }

        /// <summary>
        /// Displays the reset password page if the provided token and email are valid.
        /// </summary>
        /// <param name="Token">The password reset token.</param>
        /// <param name="Email">The user's email address.</param>
        /// <returns>An IActionResult indicating the outcome of the password reset request.</returns>
        [HttpGet]
        public IActionResult ResetPassword(string Token, string Email)
        {
            if (Token == null || Email == null)
            {
                return BadRequest("The link is expired or invalid.");
            }

            var model = new ResetPasswordRequest
            {
                Token = Token,
                Email = Email
            };

            return Ok(model);
        }

        /// <summary>
        /// Resets the user's password using the provided token and new password.
        /// </summary>
        /// <param name="model">The reset password request model containing the token, email, and new password.</param>
        /// <returns>An IActionResult indicating the outcome of the password reset process.</returns>
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
        /// <summary>
        /// Refresh token.
        /// </summary>
        /// <param name="tokenRequest">The token request model containing the access token and refresh token.</param>
        /// <returns>An IActionResult indicating the access and refresh tokens.</returns>
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDTO tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var verified = await _jwtService.VerifyToken(tokenRequest);
                //
                if (!verified.Success)
                {
                    return BadRequest(new AuthResult()
                    {
                        // Errors = new List<string> { "invalid Token" },
                        Errors = verified.Errors,
                        Success = false
                    });
                }

                var tokenUser = await _userManager.FindByIdAsync(verified.IdentityUserId)
                    ?? throw new InvalidOperationException("IdentityUserId is not configured."); ;
                AuthResult authResult = await _jwtService.GenerateToken(tokenUser);
                //return a token
                return Ok(authResult);


            }

            return BadRequest(new AuthResult()
            {
                Errors = ["invalid Payload"],
                Success = false
            });



        }
    }
}
