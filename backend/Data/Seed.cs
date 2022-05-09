using System.Text.Json;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data;

public static class Seed
{
    //Add new seeds by creating a seed data file Data/SeedData/*.json
    //and creating a Seed* method that takes the relevant services as parameters.
    public static async Task Run(IServiceScope scope)
    {
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        await SeedUsers(userManager, roleManager);
        await SeedCategories(unitOfWork);
        await SeedMenuItems(unitOfWork);
        unitOfWork.Complete();
    }

    private static async Task SeedCategories(IUnitOfWork unitOfWork)
    {
        var categoryData = await File.ReadAllTextAsync("Data/SeedData/CategorySeedData.json");
        var categories = JsonSerializer.Deserialize<List<CategoryDto>>(categoryData);

        foreach (var category in categories)
        {
            var cat = new Category(category);
            unitOfWork.CategoryRepository.Add(cat);
        }
    }

    private static async Task SeedMenuItems(IUnitOfWork unitOfWork)
    {
        var MenuItemData = await File.ReadAllTextAsync("Data/SeedData/MenuItemSeedData.json");
        var MenuItems = JsonSerializer.Deserialize<List<MenuItemNewDto>>(MenuItemData);

        foreach (var menuItem in MenuItems)
        {
            var category = await unitOfWork.CategoryRepository.GetCategory(menuItem.CategoryId);
            var item = new MenuItem
            {
                Description = menuItem.Description,
                Title = menuItem.Title,
                Price = menuItem.Price,
                Category = category
            };

            unitOfWork.MenuRepository.AddMenuItem(item);
        }
    }

    private static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var roles = new List<AppRole>
        {
            new() { Name = "Staff" },
            new() { Name = "Admin" },
            new() { Name = "Table" },
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