using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    [Table("Employee")]
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

        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100")]
        public int? Age { get; set; } = null;

        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;


        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; } = DateTime.Now;

        [Precision(18, 2)]
        public decimal Salary { get; set; } = decimal.Zero;

        public int PositionId { get; set; }
        public Position? Position { get; set; }

        public int IdentityUserId { get; set; }
        public IdentityUser? IdentityUser { get; set; }

        public List<Application>? Applications { get; set; }

        public List<EmployeeGrade>? EmployeeGrades { get; set; }

        public List<Subject>? Subjects { get; set; }

    }

    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
           => self.Select((item, index) => (item, index));
    }
}
