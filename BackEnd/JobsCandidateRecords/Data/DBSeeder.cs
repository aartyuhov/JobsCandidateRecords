using Bogus;
using JobsCandidateRecords.Enums;
using JobsCandidateRecords.Models;


namespace JobsCandidateRecords.Data
{
    /// <summary>
    /// Provides methods to seed default data into the database, including roles, users, and initial company and department records.
    /// </summary>
    public class DBSeeder
    {
        /// <summary>
        /// Seeds the default data into the database.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve the required services.</param>
        /// <param name="_context">The application database context used to interact with the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task SeedDefaultData(IServiceProvider serviceProvider, ApplicationDbContext _context)
        {
            //// Retrieve the UserManager service from the service provider
            //var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>()
            //    ?? throw new InvalidOperationException("UserManager is not initialized.");

            //// Retrieve the RoleManager service from the service provider
            //var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>()
            //    ?? throw new InvalidOperationException("RoleManager is not initialized.");

            //// Create roles if they do not exist
            //await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            //await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //// Create an admin user if it does not already exist
            //var admin = new IdentityUser
            //{
            //    UserName = "admin",
            //    Email = "admin@gmail.com",
            //    EmailConfirmed = true
            //};

            //var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
            //if (isUserExists == null)
            //{
            //    await userMgr.CreateAsync(admin, "admin");
            //    await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            //}

            //// Seed company data
            //await _context.Companies.AddRangeAsync(
            //    new Company
            //    {
            //        Name = "ITSTEP",
            //        Description = "Computer academy"
            //    }
            //);

            //// Seed department data
            //await _context.Departments.AddRangeAsync(
            //    new Department
            //    {
            //        Name = "Administration",
            //        Description = "Coordinating all administrative processes, managing budgets, policies and events, resolving conflicts or other issues as they occur",
            //        CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault()
            //    },
            //    new Department
            //    {
            //        Name = "Network and Cybersecurity",
            //        Description = "Creation and maintenance of computer networks",
            //        CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault()
            //    },
            //    new Department
            //    {
            //        Name = "Software Development",
            //        Description = "Focuses on teaching programming languages, software architecture, and development methodologies.",
            //        CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault()
            //    },
            //    new Department
            //    {
            //        Name = "Computer Graphics and Design",
            //        Description = "Create ads, take photos and videos, create animations, develop sites and applications",
            //        CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault()
            //    }
            //);

            // Save all changes to the database
            await _context.SaveChangesAsync();

            var positionFaker = new Faker<Position>("en")
                .RuleFor(p => p.Title, f => f.PickRandom<PositionsEnum>().ToString())
                .RuleFor(p => p.Responsibilities, (f, p) => "Some responsibility of " + p.Title)
                .RuleFor(p => p.DepartmentId, f => f.Random.ArrayElement(_context.Departments.Select((d) => d.Id).ToArray()));

            var positions = positionFaker.Generate(100).DistinctBy(p => p.Title).ToList();

            await _context.Positions.AddRangeAsync(positions);

            // Save all changes to the database
            await _context.SaveChangesAsync();

            var employeeFaker = new Faker<Employee>("en")
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.DateOfBirth, f => DateOnly.FromDateTime(f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).DateTime))
                .RuleFor(e => e.Gender, f => f.Person.Gender.ToString())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.HireDate, f => f.Date.PastOffset(20, DateTime.Now).DateTime)
                .RuleFor(e => e.AvatarUrl, f => f.Internet.Url())
                .RuleFor(e => e.PositionId, f => f.Random.ArrayElement(_context.Positions.Select((p) => p.Id).ToArray()));

            var employees = employeeFaker.Generate(20).ToList();
            //var employees = employeeFaker.Generate(20).DistinctBy(s => s.Name).ToList();
            await _context.Employees.AddRangeAsync(employees);

            // Save all changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
