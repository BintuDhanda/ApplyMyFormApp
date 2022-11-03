using Microsoft.AspNetCore.Identity;
using rojgar.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Utilities
{
    public class AdminAndRolesInitializer
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminAndRolesInitializer(UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<string> Initialize()
        {
            String[] Roles = { "Admin", "User" };
            foreach (var role in Roles)
            {
                var result = await roleManager.FindByNameAsync(role);
                if (result == null)
                {
                    IdentityRole newRole = new IdentityRole
                    {
                        Name = role
                    };
                    await roleManager.CreateAsync(newRole);
                }
            }
            
            var AdminCheck = await userManager.FindByNameAsync("Admin@123");
            if (AdminCheck == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    FullName = "Admin",
                    UserName = "Admin@123",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true,
                    Role = "Admin",
                };
                await userManager.CreateAsync(user, "Admin@123");
                var createdUser = await userManager.FindByNameAsync(user.UserName);
                await userManager.AddToRolesAsync(createdUser, Roles.Where(g=>g!="User"));
            }
           
            return "Done";
        }
    }
}