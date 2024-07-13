﻿using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Notes")]
    public class Note
    {
        public int Id { get; set; }

        public int? ApplicationId { get; set; }
        [SwaggerIgnore]
        public virtual Application? Application { get; set; }

        public int EmployeeId { get; set; }
        [SwaggerIgnore]
        public virtual Employee? Employee { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}