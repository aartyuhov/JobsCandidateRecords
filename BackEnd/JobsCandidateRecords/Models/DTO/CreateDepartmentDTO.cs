namespace JobsCandidateRecords.Models.DTO
{
    public class CreateDepartmentDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}