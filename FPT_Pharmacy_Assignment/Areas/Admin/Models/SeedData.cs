using Microsoft.AspNetCore.Identity;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = ["Admin", "Moderator", "User"];
            foreach (var roleName in roleNames)
            {
                // If the role doesn't exist, create it
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}