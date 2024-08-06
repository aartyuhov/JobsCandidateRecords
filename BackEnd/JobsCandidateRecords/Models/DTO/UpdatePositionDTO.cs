using System;
using System.ComponentModel.DataAnnotations;

namespace JobsCandidateRecords.Models.DTO
{
	public class UpdatePositionDTO
	{
		[Required]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Title { get; set; } = string.Empty;

		[MaxLength(255)]
		public string? Responsibilities { get; set; }

		[Required]
		public int DepartmentId { get; set; }
	}
}