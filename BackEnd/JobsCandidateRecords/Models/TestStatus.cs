using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("TestStatus")]
    [Index(nameof(Name), IsUnique = true)]
    public class TestStatus
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public List<TestTaken>? TestTakens { get; set; }


    }
}
