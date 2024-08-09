using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Data
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or sets the academic subjects.
        /// </summary>
        public DbSet<AcademicSubject> AcademicSubjects { get; set; }

        /// <summary>
        /// Gets or sets the applications for requests.
        /// </summary>
        public DbSet<ApplicationsForRequests> ApplicationsForRequests { get; set; }

        /// <summary>
        /// Gets or sets the applications.
        /// </summary>
        public DbSet<Application> Applications { get; set; }

        /// <summary>
        /// Gets or sets the application status histories.
        /// </summary>
        public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        public DbSet<Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the candidates.
        /// </summary>
        public DbSet<Candidate> Candidates { get; set; }

        /// <summary>
        /// Gets or sets the companies.
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public DbSet<Note> Notes { get; set; }

        /// <summary>
        /// Gets or sets the positions.
        /// </summary>
        public DbSet<Position> Positions { get; set; }

        /// <summary>
        /// Gets or sets the position academic subjects.
        /// </summary>
        public DbSet<PositionAcademicSubject> PositionAcademicSubjects { get; set; }

        /// <summary>
        /// Gets or sets the requests for employees.
        /// </summary>
        public DbSet<RequestForEmployee> RequestsForEmployees { get; set; }
        /// <summary>
        /// Gets or sets the requests for refresh tokens.
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
