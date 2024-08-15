using JobsCandidateRecords.Models.DTO.Auth;
using JobsCandidateRecords.Models.DTO.Auth.Request;
using JobsCandidateRecords.Models.DTO.Auth.Response;
using Microsoft.AspNetCore.Identity;

namespace JobsCandidateRecords.Services
{
    /// <summary>
    /// Defines the contract for JWT token operations, including generation and verification of tokens.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JSON Web Token (JWT) for the specified user.
        /// </summary>
        /// <param name="user">The identity user for whom the token is to be generated.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the authentication result including the token.</returns>
        Task<AuthResult> GenerateToken(IdentityUser user);

        /// <summary>
        /// Verifies a refresh token and generates a response containing information about the token's validity.
        /// </summary>
        /// <param name="tokenRequest">The request containing the refresh token to be verified.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the response data about the refresh token.</returns>
        Task<RefreshTokenResponseDTO> VerifyToken(TokenRequestDTO tokenRequest);
    }
}
