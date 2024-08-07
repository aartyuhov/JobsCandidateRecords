namespace JobsCandidateRecords.Config
{
    public class JwtConfig
    {
        public string? Secret { get; set; } = string.Empty;

        public double AccessTokenExpirationMinutes { get; set; } = 1;

        public int RefreshTokenExpirationMonths { get; set; } = 1;

    }
}
