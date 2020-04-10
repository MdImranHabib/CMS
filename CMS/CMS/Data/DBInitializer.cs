using CMS.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class DBInitializer
    {
         public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            InitializeBranch(context);
            InitializeEmployee(context);
            await InitializeRole(roleManager);
            await InitializeUserandAssignRole(userManager);
        }

        private static void InitializeBranch(ApplicationDbContext context)
        {
            var branch = new Branch()
            {
                Name = "HeadBrach",
                Address = "Dhaka",
                Email = "headbranch@gmail.com",
                Contact = "01700000000"
            };

            context.Add(branch);
            context.SaveChanges();
        }

        private static void InitializeEmployee(ApplicationDbContext context)
        {
            var employee = new Employee()
            {
                Name = "Admin",
                Address = "HeadOffice",
                Email = "admin@gmail.com",
                Contact = "01700000000",
                BranchId = 1
            };

            context.Add(employee);
            context.SaveChanges();
        }

        private static async Task InitializeRole(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }

        private static async Task InitializeUserandAssignRole(UserManager<ApplicationUser> userManager)
        {
            if (await userManager.FindByNameAsync("admin@gmail.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "Admin2@");
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

    }
}
