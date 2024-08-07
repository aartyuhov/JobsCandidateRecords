using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsCandidateRecords.Models
{
    /// <summary>
    /// Represents an employee.
    /// </summary>
    [Table("Employees")]
    [Index(nameof(Email), IsUnique = true)]
    public class Employee
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of birth of the employee.
        /// </summary>
        public DateOnly? DateOfBirth { get; set; } = null;

        /// <summary>
        /// Gets or sets the gender of the employee.
        /// </summary>
        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the employee.
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the employee.
        /// </summary>
        [Required]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hire date of the employee.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the URL of the employee's avatar.
        /// </summary>
        public string? AvatarUrl {  get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the position held by the employee.
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the position held by the employee.
        /// </summary>
        [SwaggerIgnore]
        public virtual Position? Position { get; set; }

        /// <summary>
        /// Gets or sets the ID of the identity user associated with the employee.
        /// </summary>
        public string? IdentityUserId { get; set; }

        /// <summary>
        /// Gets or sets the identity user associated with the employee.
        /// </summary>
        [ForeignKey(nameof(IdentityUserId))]
        [SwaggerIgnore]
        public virtual IdentityUser? IdentityUser { get; set; }

        /// <summary>
        /// Gets or sets the notes associated with the employee.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Note>? Notes { get; set; }

        /// <summary>
        /// Gets or sets the applications associated with the employee.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<Application>? Applications { get; set; }

        /// <summary>
        /// Gets or sets the requests for employees associated with the employee.
        /// </summary>
        [SwaggerIgnore]
        public virtual ICollection<RequestForEmployee>? RequestsForEmployees { get; set; }
    }

    /// <summary>
    /// Extension methods for IEnumerable.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Adds an index to each item in the enumerable collection.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="self">The enumerable collection.</param>
        /// <returns>A collection of tuples containing the item and its index.</returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
           => self.Select((item, index) => (item, index));
    }
}
