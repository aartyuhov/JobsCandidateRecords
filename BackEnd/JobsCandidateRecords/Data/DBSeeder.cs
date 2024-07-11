using JobsCandidateRecords.Enums;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace JobsCandidateRecords.Data
{
    public class DBSeeder
    {
        private readonly ApplicationDbContext _context;

        public DBSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        //public static async Task SeedDefaultData(IServiceProvider serviceProvider, ApplicationDbContext _context)
        //{
        //    var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>();
        //    var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>();
        //    //add some roles to DB
        //    await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        //    await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
        //    //create admin user
        //    var admin = new IdentityUser
        //    {
        //        UserName = "admin",
        //        Email = "admin@gmail.com",
        //        EmailConfirmed = true
        //    };
        //    var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
        //    if (isUserExists == null)
        //    {
        //        await userMgr.CreateAsync(admin, "admin");
        //        var result = await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
        //    }

        //    await _context.Departments.AddRangeAsync(
        //         new Department { Name = "Administration", Description = "Coordinating all administrative processes, managing budgets, policies and events, resolving conflicts or other issues as they occur" },
        //         new Department { Name = "Networkinq and Cybersecurity", Description = "Creation and maintenance of computer networks" },
        //         new Department { Name = "Software development", Description = "Focuses on teaching programming languages, software architecture, and development methodologies." },
        //         new Department { Name = "Computer graphics and design", Description = "Create ads, take photos and videos, create animations, develop sites and applications" }
        //         );

        //    await _context.Subjects.AddRangeAsync(
        //         new Subject { Name = "Artificial intelligence", Description = "AI technologies for solving creative tasks. Branding and design.", DepartmentId = 4 },
        //         new Subject { Name = "UX/UI design", Description = "Creating a prototype from scratch. Animation for UX/UI", DepartmentId = 4 },
        //         new Subject { Name = "Networks, basic course", Description = "Understand all the IT Networking Fundamentals and learn how the computer network functions", DepartmentId = 2 },
        //         new Subject { Name = "Cloud technologies and DevOps tools", Description = "The course covers a wide range of essential topics, ensuring you will understand cloud technologies and DevOps practices holistically", DepartmentId = 2 },
        //         new Subject { Name = "ASP.NET", Description = "Development of web applications using ASP.NET Core in C# using MS SQL Server DBMS", DepartmentId = 3 },
        //         new Subject { Name = "C++", Description = "Object-oriented programming using the C++ language", DepartmentId = 3 },
        //         new Subject { Name = "PHP", Description = "Learning PHP. PHP is a server scripting language, and a powerful tool for making dynamic and interactive Web pages", DepartmentId = 3 },
        //         new Subject { Name = "Python", Description = "Learning Python", DepartmentId = 3 }
        //         );

        //    await _context.Courses.AddRangeAsync(
        //        new Course { Name = "Python Developer", StartDate = DateTime.ParseExact("2024.09.01", "yyyy.MM.dd", CultureInfo.CurrentCulture), EndDate = DateTime.ParseExact("2025.04.01", "yyyy.MM.dd", CultureInfo.CurrentCulture), Description = "Writing code to create websites and applications or to work with data and artificial intelligence.", SubjectId = 8 },
        //        new Course { Name = "Front-end development", StartDate = DateTime.ParseExact("2024.08.15", "yyyy.MM.dd", CultureInfo.CurrentCulture), EndDate = DateTime.ParseExact("2025.02.15", "yyyy.MM.dd", CultureInfo.CurrentCulture), Description = "You'll start with the basics of HTML and CSS layout. Learn JavaScript and frameworks, and by the end of the course you'll learn how to make web pages and corporate services.", SubjectId = 7 },
        //        new Course { Name = "DevOps engineer", StartDate = DateTime.ParseExact("2024.09.20", "yyyy.MM.dd", CultureInfo.CurrentCulture), EndDate = DateTime.ParseExact("2025.05.20", "yyyy.MM.dd", CultureInfo.CurrentCulture), Description = "After training as a DevOps engineer, you will acquire confident skills in installing servers from scratch, configuring networks, preparing operating systems, monitoring processes, including using the Python programming language.", SubjectId = 3 }
        //        );




        //    await _context.SaveChangesAsync();
        //}
    }
}
