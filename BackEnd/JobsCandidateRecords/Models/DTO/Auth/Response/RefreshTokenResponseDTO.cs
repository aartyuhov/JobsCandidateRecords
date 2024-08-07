namespace JobsCandidateRecords.Models.DTO.Auth.Response
{
    public class RefreshTokenResponseDTO
    {
        public string IdentityUserId { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
