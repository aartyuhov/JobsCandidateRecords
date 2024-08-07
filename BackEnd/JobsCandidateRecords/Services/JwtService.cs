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
    public class JwtService : IJwtService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ApplicationDbContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<IdentityUser> _userManager;
        public JwtService(IOptionsMonitor<JwtConfig> jwtConfig, ApplicationDbContext context, TokenValidationParameters tokenValidationParameters, UserManager<IdentityUser> userManager)
        {
            _jwtConfig = jwtConfig.CurrentValue;
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
            _userManager = userManager;
        }

        public async Task<AuthResult> GenerateToken(IdentityUser user)
        {

            JwtSecurityTokenHandler? jwtTokenHandler = new JwtSecurityTokenHandler();

            Byte[] key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var userRoles = _userManager.GetRolesAsync(user).Result;

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Role, string.Join(",", userRoles)),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.AccessTokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token
            SecurityToken? token = jwtTokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = jwtTokenHandler.WriteToken(token);

            // Create refresh token
            RefreshToken refreshToken = new RefreshToken()
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

        public async Task<RefreshTokenResponseDTO> VerifyToken(TokenRequestDTO tokenRequest)
        {
            JwtSecurityTokenHandler? jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ////////////////
                RefreshToken? storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == tokenRequest.RefreshToken);
                if (storedToken == null)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = new List<string>{
                     "refersh token does not found"
                    }
                    };
                }
                //ClaimsPrincipal? tokenVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters, out var validatedToken); //?

                ////////////////
                //var jti = tokenVerification.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Jti).Value;

                //if (storedToken.JwtId != jti)
                //{
                //    return new RefreshTokenResponseDTO()
                //    {
                //        Success = false,
                //        Errors = new List<string>{
                //     "refresh token doesn't match"
                //    }
                //    };
                //}

                //////////////////
                //long expValue = long.Parse(tokenVerification.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp).Value);

                // UTC to DateTime
                //DateTime expireDate = UTCtoDateTime(expValue);
                //DateTime expDate = DateTimeOffset.FromUnixTimeSeconds(expValue).DateTime;

                //if (expireDate > DateTime.UtcNow)
                if (storedToken.ExpiredAt < DateTime.UtcNow)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = new List<string>{
                        "Token was expired"
                    }
                    };
                }

                //////////////////
                //if (validatedToken is JwtSecurityToken jwtSecurityToken)
                //{
                //    bool result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);//?

                //    if (!result)
                //    {
                //        return null;
                //    }
                //}
                ////////////////
                if (storedToken.IsRevoked)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = new List<string>{
                     "token revoked."
                    }
                    };
                }
                //////////////////
                if (storedToken.IsUsed)
                {
                    return new RefreshTokenResponseDTO()
                    {
                        Success = false,
                        Errors = new List<string>{
                     "token used."
                    }
                    };
                }

                ////////////////
                storedToken.IsUsed = true;
                _context.RefreshTokens.Update(storedToken);
                await _context.SaveChangesAsync();

                // return token
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
                    Errors = new List<string>{
                    e.Message
                },
                    Success = false
                };
            }



        }

        private DateTime UTCtoDateTime(long unixTimeStamp)
        {
            var datetimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            datetimeVal = datetimeVal.AddSeconds(unixTimeStamp).ToLocalTime();

            return datetimeVal;
        }

        private string GetRandomString()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPRSTUVYZWX0123456789";
            return new string(Enumerable.Repeat(chars, 35).Select(n => n[new Random().Next(n.Length)]).ToArray());

        }
    }
}
