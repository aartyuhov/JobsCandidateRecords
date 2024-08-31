using JobsCandidateRecords.Config;
using JobsCandidateRecords.Data;
using JobsCandidateRecords.Models;
using JobsCandidateRecords.Models.DTO.Auth;
using JobsCandidateRecords.Models.DTO.Auth.Request;
using JobsCandidateRecords.Models.DTO.Auth.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// JwtService is responsible for generating and verifying JWT tokens and managing refresh tokens.
    /// </summary>
    public class JwtService(IOptionsMonitor<JwtConfig> jwtConfig, ApplicationDbContext context, /*TokenValidationParameters tokenValidationParameters,*/ UserManager<IdentityUser> userManager) : IJwtService
    {
        private readonly JwtConfig _jwtConfig = jwtConfig.CurrentValue;
        private readonly ApplicationDbContext _context = context;
        //private readonly TokenValidationParameters _tokenValidationParameters = tokenValidationParameters;
        private readonly UserManager<IdentityUser> _userManager = userManager;

        /// <summary>
        /// Generates a JWT token and a refresh token for a given user.
        /// </summary>
        /// <param name="user">The user for whom the tokens are to be generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the authentication result including tokens.</returns>
        public async Task<AuthResult> GenerateToken(IdentityUser user)
        {
            JwtSecurityTokenHandler? jwtTokenHandler = new();

            if (_jwtConfig.Secret == null)
            {
                throw new InvalidOperationException("_jwtConfig.Secret cannot be null.");
            }

            Byte[] key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var userRoles = _userManager.GetRolesAsync(user).Result;

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(
                [
                new Claim("Id", user.Id),
            new Claim(ClaimTypes.Role, string.Join(",", userRoles)),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ]),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token
            SecurityToken? token = jwtTokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = jwtTokenHandler.WriteToken(token);

            // Create refresh token
            RefreshToken refreshToken = new()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                IdentityUserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMonths(_jwtConfig.RefreshTokenExpirationMonths),
                Token = GetRandomString() + Guid.NewGuid()
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                Success = true,
            };
        }

        /// <summary>
        /// Verifies a refresh token and returns the associated user ID if valid.
        /// </summary>
        /// <param name="tokenRequest">The request containing the refresh token to verify.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response including the user ID and success status.</returns>
        public async Task<RefreshTokenResponseDTO> VerifyToken(TokenRequestDTO tokenRequest)
        {
            JwtSecurityTokenHandler? jwtTokenHandler = new();

            try
            {
                RefreshToken? storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == tokenRequest.RefreshToken);
                if (storedToken == null)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = [
                            "refresh token does not found"
                        ]
                    };
                }

                if (storedToken.ExpiredAt < DateTime.UtcNow)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = [
                            "Token was expired"
                        ]
                    };
                }

                if (storedToken.IsRevoked)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = [
                            "token revoked."
                        ]
                    };
                }

                if (storedToken.IsUsed)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = [
                            "token used."
                        ]
                    };
                }

                storedToken.IsUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                if (storedToken.IdentityUserId == null)
                {
                    throw new InvalidOperationException("IdentityUserId cannot be null.");
                }

                // Return token
                return new RefreshTokenResponseDTO()
                {
                    IdentityUserId = storedToken.IdentityUserId,
                    Success = true,
                };
            }
            catch (Exception e)
            {
                return new RefreshTokenResponseDTO()
                {
                    Errors = [
                        e.Message
                    ],
                    Success = false
                };
            }
        }

        /*/// <summary>
        /// Converts a Unix timestamp to a DateTime in UTC.
        /// </summary>
        /// <param name="unixTimeStamp">The Unix timestamp to convert.</param>
        /// <returns>The corresponding DateTime in UTC.</returns>
        private static DateTime UTCtoDateTime(long unixTimeStamp)
        {
            var datetimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            datetimeVal = datetimeVal.AddSeconds(unixTimeStamp).ToLocalTime();

            return datetimeVal;
        }*/

        /// <summary>
        /// Generates a random string of 35 characters for use in tokens.
        /// </summary>
        /// <returns>A random string.</returns>
        private static string GetRandomString()
        {
            Random random = new();
            string chars = "ABCDEFGHIJKLMNOPRSTUVYZWX0123456789";
            return new string(Enumerable.Repeat(chars, 35).Select(n => n[new Random().Next(n.Length)]).ToArray());
        }
    }

}
