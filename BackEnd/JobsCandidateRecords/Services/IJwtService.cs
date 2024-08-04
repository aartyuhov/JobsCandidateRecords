using JobsCandidateRecords.Models.DTO.Auth;
using JobsCandidateRecords.Models.DTO.Auth.Request;
using JobsCandidateRecords.Models.DTO.Auth.Response;
using Microsoft.AspNetCore.Identity;

namespace JobsCandidateRecords.Services
{
    public interface IJwtService
    {
        Task<AuthResult> GenerateToken(IdentityUser user);
        Task<RefreshTokenResponseDTO> VerifyToken(TokenRequestDTO tokenRequest);
    }
}
