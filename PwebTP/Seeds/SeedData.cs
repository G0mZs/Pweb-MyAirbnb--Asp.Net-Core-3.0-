using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PwebTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PwebTP.Seeds
{
    public class SeedData
    {
        public static async Task InitializeAsync(
            IServiceProvider services)
        {
            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();

            await CreateRolesAsync(roleManager);

            var userManager = services
                .GetRequiredService<UserManager<ApplicationUser>>();
            await EnsureTestAdminAsync(userManager);
        }

        private static async Task CreateRolesAsync(
    RoleManager<IdentityRole> roleManager)
        {
            string[] RoleNames = { "Administrator", "Manager", "Client", "Employee" };

            foreach (var Role in RoleNames)
            {
                var alreadyExists = await roleManager
                    .RoleExistsAsync(Role);

                if (alreadyExists) return;

                await roleManager.CreateAsync(
                    new IdentityRole(Role));
            }
        }

        private static async Task EnsureTestAdminAsync(
    UserManager<ApplicationUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == "admin@todo.local")
                .SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new ApplicationUser
            {
                UserName = "admin@todo.local",
                Email = "admin@todo.local",
                Role ="Administrator"
            };
            await userManager.CreateAsync(
                testAdmin, "NotSecure123!!");
            await userManager.AddToRoleAsync(
                testAdmin, "Administrator");
        }
    }
}
