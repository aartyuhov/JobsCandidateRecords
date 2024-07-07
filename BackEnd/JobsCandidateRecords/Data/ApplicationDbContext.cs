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
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
        public DbSet<AppliedFor> AppliedFors { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeGrade> EmployeeGrades { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PostedOn> PostedOns { get; set; }
        public DbSet<RelatedDocument> RelatedDocuments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TestStatus> TestStatuses { get; set; }
        public DbSet<TestTaken> TestTakens { get; set; }

    }
}
