namespace JobsCandidateRecords.Data
{
    public class DBSeeder
    {
        private readonly ApplicationDbContext _context;

        public DBSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public static async Task SeedDefaultData(IServiceProvider serviceProvider, ApplicationDbContext _context)
        {
            //var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>();
            //var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>();
            ////add some roles to DB
            //await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            //await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            ////create admin user
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
            //    var result = await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            //}

            //await _context.Companies.AddRangeAsync(
            //    new Company { Name = "ITSTEP", Description = "Computer academy" }
            //    );

            //await _context.Departments.AddRangeAsync(
            //     new Department { Name = "Administration", Description = "Coordinating all administrative processes, managing budgets, policies and events, resolving conflicts or other issues as they occur", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() },
            //     new Department { Name = "Networkinq and Cybersecurity", Description = "Creation and maintenance of computer networks", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() },
            //     new Department { Name = "Software development", Description = "Focuses on teaching programming languages, software architecture, and development methodologies.", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() },
            //     new Department { Name = "Computer graphics and design", Description = "Create ads, take photos and videos, create animations, develop sites and applications", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() }
            //     );


            //await _context.AcademicSubjects.AddRangeAsync(
            //     new AcademicSubject { Name = "Artificial intelligence", Description = "AI technologies for solving creative tasks. Branding and design." },
            //     new AcademicSubject { Name = "UX/UI design", Description = "Creating a prototype from scratch. Animation for UX/UI" },
            //     new AcademicSubject { Name = "Networks, basic course", Description = "Understand all the IT Networking Fundamentals and learn how the computer network functions" },
            //     new AcademicSubject { Name = "Cloud technologies and DevOps tools", Description = "The course covers a wide range of essential topics, ensuring you will understand cloud technologies and DevOps practices holistically" },
            //     new AcademicSubject { Name = "ASP.NET", Description = "Development of web applications using ASP.NET Core in C# using MS SQL Server DBMS" },
            //     new AcademicSubject { Name = "C++", Description = "Object-oriented programming using the C++ language" },
            //     new AcademicSubject { Name = "PHP", Description = "Learning PHP. PHP is a server scripting language, and a powerful tool for making dynamic and interactive Web pages" },
            //     new AcademicSubject { Name = "Python", Description = "Learning Python" }
            //     );


            //var positionFaker = new Faker<Position>("en")
            //    .RuleFor(p => p.Title, f => f.PickRandom<PositionsEnum>().ToString())
            //    .RuleFor(p => p.Responsibilities, (f, p) => "Some responsibility of " + p.Title)
            //    .RuleFor(p => p.DepartmentId, f => f.Random.ArrayElement(_context.Departments.Select((d) => d.Id).ToArray()));

            //var positions = positionFaker.Generate(100).DistinctBy(p => p.Title).ToList();

            //await _context.Positions.AddRangeAsync(positions);

            //var employeeFaker = new Faker<Employee>("en")
            //    .RuleFor(e => e.FirstName, f => f.Name.FirstName())
            //    .RuleFor(e => e.LastName, f => f.Name.LastName())
            //    .RuleFor(e => e.DateOfBirth, f => DateOnly.FromDateTime(f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).DateTime))
            //    .RuleFor(e => e.Gender, f => f.Person.Gender.ToString())
            //    .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
            //    .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
            //    .RuleFor(e => e.Address, f => f.Address.FullAddress())
            //    .RuleFor(e => e.HireDate, f => f.Date.PastOffset(20, DateTime.Now).DateTime)
            //    .RuleFor(e => e.AvatarUrl, f => f.Internet.Url())
            //    .RuleFor(e => e.PositionId, f => f.Random.ArrayElement(_context.Positions.Select((p) => p.Id).ToArray()));

            //var employees = employeeFaker.Generate(20).ToList();
            ////var employees = employeeFaker.Generate(20).DistinctBy(s => s.Name).ToList();
            //await _context.Employees.AddRangeAsync(employees);
            //await _context.SaveChangesAsync();
        }
    }
}
