using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Document")]
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(255)]
        public string Location { get; set; } = string.Empty;

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public List<RelatedDocument>? RelatedDocuments { get; set; }

    }
}
