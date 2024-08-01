namespace JobsCandidateRecords.Models.DTO
{
    public class UpdateCompanyDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}