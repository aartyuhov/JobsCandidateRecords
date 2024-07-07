using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("PostedOn")]
    public class PostedOn
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Link { get; set; } = string.Empty;

        public int JobId { get; set; }
        public Job? Job { get; set; }
    }
}
