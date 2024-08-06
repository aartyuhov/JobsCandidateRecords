using System.ComponentModel.DataAnnotations;
namespace JobsCandidateRecords.Models.DTO
{
    public class UpdateDepartmentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}