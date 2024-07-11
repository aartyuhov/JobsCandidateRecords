using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Notes")]
    public class Notes
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public Application? Application { get; set; }

        [MaxLength(255)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
