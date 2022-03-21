using System.Text.Json;
using foodies_app.DTOs;
using foodies_app.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data;

public class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var roles = new List<AppRole>
        {
            new() {Name = "Staff"},
            new() {Name = "Admin"},
            new() {Name = "Table"},
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        var userData = await File.ReadAllTextAsync("Data/SeedData/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<RegisterDto>>(userData);

        foreach (var user in users)
        {
            var appUser = new AppUser
            {
                UserName = user.UserName.ToLower()
            };
            await userManager.CreateAsync(appUser, "Passw0rd!");
            await userManager.AddToRoleAsync(appUser, user.Role);
        }
    }
}