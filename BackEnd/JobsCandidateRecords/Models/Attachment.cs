using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Attachments")]
    public class Attachment
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        [Required]
        public string FileName { get; set; } = string.Empty; //The name of the file that is saved in the BLOB container

        public int ApplicationId { get; set; }
        [SwaggerIgnore]
        public virtual Application? Application { get; set; }
        [SwaggerIgnore]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
