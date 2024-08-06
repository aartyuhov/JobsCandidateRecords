using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.DTO
{
    public class CreateCompanyDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}