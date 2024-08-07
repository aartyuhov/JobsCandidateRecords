using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents an refresh tokens.
    /// </summary>
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the unique identifier for the refresh token.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the identity user associated with the refresh token.
        /// </summary>
        public string? IdentityUserId { get; set; } = null;
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string? Token { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the ID of jwt token.
        /// </summary>
        public string? JwtId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Used state of refresh token.
        /// </summary>
        public bool IsUsed { get; set; } = false;
        /// <summary>
        /// Gets or sets the Revoked state of refresh token.
        /// </summary>
        public bool IsRevoked { get; set; } = false;
        /// <summary>
        /// Gets or sets the create date of refresh token.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        /// <summary>
        /// Gets or sets the expired date of refresh token.
        /// </summary>
        public DateTime ExpiredAt { get; set; }

        [ForeignKey(nameof(IdentityUserId))]
        [SwaggerIgnore]
        public IdentityUser? IdentityUser { get; set; }
    }
}
