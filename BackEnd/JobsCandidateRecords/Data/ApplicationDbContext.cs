using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobsCandidateRecords.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AcademicSubject> AcademicSubjects { get; set; }
        public DbSet<ApplicationsForRequests> ApplicationsForRequests { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionAcademicSubject> PositionAcademicSubjects { get; set; }
        public DbSet<RequestForEmployee> RequestsForEmployees { get; set; }
    }
}
