using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("RelatedDocument")]
    public class RelatedDocument
    {
        public int Id { get; set; }

        [Required]
        public int DocumentId { get; set; }
        public Document? Document { get; set; }

        [Required]
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
    }
}
