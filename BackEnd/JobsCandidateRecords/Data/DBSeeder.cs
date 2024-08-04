using JobsCandidateRecords.Enums;
using JobsCandidateRecords.Models;
using Microsoft.AspNetCore.Identity;

namespace JobsCandidateRecords.Data
{
    public class DBSeeder(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public static async Task SeedDefaultData(IServiceProvider serviceProvider, ApplicationDbContext _context)
        {
            var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>();
            //add some roles to DB
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            //create admin user
            var admin = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };
            var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
            if (isUserExists == null)
            {
                await userMgr.CreateAsync(admin, "admin");
                var result = await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }

            await _context.Companies.AddRangeAsync(
                new Company { Name = "ITSTEP", Description = "Computer academy" }
                );

            await _context.Departments.AddRangeAsync(
                 new Department { Name = "Administration", Description = "Coordinating all administrative processes, managing budgets, policies and events, resolving conflicts or other issues as they occur", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() },
                 new Department { Name = "Networkinq and Cybersecurity", Description = "Creation and maintenance of computer networks", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() },
                 new Department { Name = "Software development", Description = "Focuses on teaching programming languages, software architecture, and development methodologies.", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() },
                 new Department { Name = "Computer graphics and design", Description = "Create ads, take photos and videos, create animations, develop sites and applications", CompanyId = _context.Companies.Where(c => c.Name == "ITSTEP").Select(c => c.Id).FirstOrDefault() }
                 );

            await _context.SaveChangesAsync();
        }
    }
}
