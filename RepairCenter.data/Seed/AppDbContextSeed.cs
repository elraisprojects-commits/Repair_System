using Microsoft.AspNetCore.Identity;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Seed
{
    public static class AppDbContextSeed
    {
        public static async Task SeedRolesAndAdminAsync(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Roles

            string[] roles =
            {
                "Admin",
                "Receptionist",
                "Specialist"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }

            // Branches

            if (!context.Branches.Any())
            {
                context.Branches.AddRange(
                    new Branch
                    {
                        Name = "dokki"
                      
                    },
                    
                    new Branch
                    {
                        Name = "nasr city"
                       
                    });

                await context.SaveChangesAsync();
            }

            // Admin

            var adminUser =
                await userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@repair.com",
                    FullName = "System Admin"
                };

                await userManager.CreateAsync(
                    adminUser,
                    "Admin@123");

                await userManager.AddToRoleAsync(
                    adminUser,
                    "Admin");
            }

            // Receptionist

            var receptionUser =
                await userManager.FindByNameAsync("reception");

            if (receptionUser == null)
            {
                receptionUser = new ApplicationUser
                {
                    UserName = "reception",
                    Email = "reception1@repair.com",
                    FullName = "Reception"
                };

                await userManager.CreateAsync(
                    receptionUser,
                    "Reception@123");

                await userManager.AddToRoleAsync(
                    receptionUser,
                    "Receptionist");
            }

            // Specialist

            var specialistUser =
                await userManager.FindByNameAsync("specialist");

            if (specialistUser == null)
            {
                specialistUser = new ApplicationUser
                {
                    UserName = "specialist",
                    Email = "specialist1@repair.com",
                    FullName = "Specialist"
                };

                await userManager.CreateAsync(
                    specialistUser,
                    "Specialist@123");

                await userManager.AddToRoleAsync(
                    specialistUser,
                    "Specialist");
            }
        }
    }
}