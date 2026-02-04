using Microsoft.AspNetCore.Identity;

namespace QuizMaster.Data.Seed;

public class RoleSeeder
{
    public static async Task SeedRolesAsync(IServiceProvider  serviceProvider)  
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roleNames = { "Teacher", "Student" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}