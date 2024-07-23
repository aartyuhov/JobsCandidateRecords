using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Employees")]
    [Index(nameof(Email), IsUnique = true)]
    public class Employee
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        public DateOnly? DateOfBirth { get; set; } = null;

        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; } = DateTime.Now;

        public string? AvatarUrl {  get; set; } = string.Empty;

        public int PositionId { get; set; }
        [SwaggerIgnore]
        public virtual Position? Position { get; set; }

        public string? IdentityUserId { get; set; }
        [ForeignKey(nameof(IdentityUserId))]
        [SwaggerIgnore]
        public virtual IdentityUser? IdentityUser { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<Note>? Notes { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<Application>? Applications { get; set; }
        [SwaggerIgnore]
        public virtual ICollection<RequestForEmployee>? RequestsForEmployees { get; set; }
    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
           => self.Select((item, index) => (item, index));
    }
}
