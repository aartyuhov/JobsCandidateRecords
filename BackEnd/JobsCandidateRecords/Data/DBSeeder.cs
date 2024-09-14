using Bogus;
using JobsCandidateRecords.Enums;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Identity;


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
        public static void SeedDefaultData(IServiceProvider serviceProvider, ApplicationDbContext _context)
        {
            try
            {
                var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>() ?? throw new InvalidOperationException("UserManager is not initialized.");
                var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>() ?? throw new InvalidOperationException("RoleManager is not initialized.");

                // Create roles if they do not exist
                EnsureRolesExist(roleMgr);

                // Seed company and department data
                SeedCompanyAndDepartments(_context);

                // Seed positions
                SeedPositions(_context);

                // Seed admin user if it does not already exist
                SeedAdminUser(userMgr, _context);

                // Seed employees
                SeedEmployees(_context);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during database seeding: {ex.Message}");
                throw; // Rethrow to indicate failure
            }
        }

        private static void EnsureRolesExist(RoleManager<IdentityRole> roleMgr)
        {
            if (!roleMgr.RoleExistsAsync(Roles.Admin.ToString()).Result)
            {
                roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString())).Wait();
            }
            if (!roleMgr.RoleExistsAsync(Roles.User.ToString()).Result)
            {
                roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString())).Wait();
            }
        }

        private static void SeedAdminUser(UserManager<IdentityUser> userMgr, ApplicationDbContext _context)
        {
            var adminEmail = "admin@gmail.com";
            var admin = userMgr.FindByEmailAsync(adminEmail).Result;

            if (admin == null)
            {
                admin = new IdentityUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                userMgr.CreateAsync(admin, "admin").Wait();
                userMgr.AddToRoleAsync(admin, Roles.Admin.ToString()).Wait();

                var adminEmployee = new Employee
                {
                    FirstName = "admin",
                    LastName = "admin",
                    Address = "address",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Now.Date),
                    AvatarUrl = string.Empty,
                    Email = admin.Email,
                    Gender = "Male",
                    HireDate = DateTime.Now,
                    PhoneNumber = "+33333333333333",
                    PositionId = _context.Positions.First(p => p.Title == "Developer").Id,
                    IdentityUserId = admin.Id
                };

                _context.Employees.Add(adminEmployee);
                _context.SaveChanges();
            }
        }

        private static void SeedCompanyAndDepartments(ApplicationDbContext _context)
        {
            var itstepCompany = new Company
            {
                Name = "ITSTEP",
                Description = "Computer academy"
            };

            if (!_context.Companies.Any(c => c.Name == "ITSTEP"))
            {
                _context.Companies.Add(itstepCompany);
                _context.SaveChanges();
            }

            var itstepCompanyId = itstepCompany.Id;

            var departments = new List<Department>
    {
        new Department
        {
            Name = "Administration",
            Description = "Coordinating all administrative processes, managing budgets, policies and events, resolving conflicts or other issues as they occur",
            CompanyId = itstepCompanyId
        },
        new Department
        {
            Name = "Network and Cybersecurity",
            Description = "Creation and maintenance of computer networks",
            CompanyId = itstepCompanyId
        },
        new Department
        {
            Name = "Software Development",
            Description = "Focuses on teaching programming languages, software architecture, and development methodologies.",
            CompanyId = itstepCompanyId
        },
        new Department
        {
            Name = "Computer Graphics and Design",
            Description = "Create ads, take photos and videos, create animations, develop sites and applications",
            CompanyId = itstepCompanyId
        }
    };

            foreach (var department in departments)
            {
                if (!_context.Departments.Any(d => d.Name == department.Name))
                {
                    _context.Departments.Add(department);
                }
            }

            _context.SaveChanges();
        }

        private static void SeedPositions(ApplicationDbContext _context)
        {
            var positionFaker = new Faker<Position>("en")
                .RuleFor(p => p.Title, f => f.PickRandom<PositionsEnum>().ToString())
                .RuleFor(p => p.Responsibilities, (f, p) => "Some responsibility of " + p.Title)
                .RuleFor(p => p.DepartmentId, f => f.Random.ArrayElement(_context.Departments.Select(d => d.Id).ToArray()));

            var positions = positionFaker.Generate(100).DistinctBy(p => p.Title).ToList();

            _context.Positions.AddRange(positions);
            _context.SaveChanges();
        }

        private static void SeedEmployees(ApplicationDbContext _context)
        {
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
                .RuleFor(e => e.PositionId, f => f.Random.ArrayElement(_context.Positions.Select(p => p.Id).ToArray()));

            var employees = employeeFaker.Generate(20).ToList();

            _context.Employees.AddRange(employees);
            _context.SaveChanges();
        }


    }
}
