using System.Text.Json;
using foodies_app.Data.Repositories;
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
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
        var menuItemRepository = scope.ServiceProvider.GetRequiredService<IMenuItemRepository>();
        await SeedUsers(userManager, roleManager);
        await SeedCategories(categoryRepository);
        await SeedMenuItems(menuItemRepository);
    }

    private static async Task SeedCategories(ICategoryRepository categoryRepository)
    {
        var CategoryData = await File.ReadAllTextAsync("Data/SeedData/CategorySeedData.json");
        var categories = JsonSerializer.Deserialize<List<CategoryDTO>>(CategoryData);

        foreach (var Category in categories)
        {
            Category Cat = new Category(Category);
            categoryRepository.Add(Cat);
        }
    }

    private static async Task SeedMenuItems(IMenuItemRepository menuItemRepository)
    {
        var MenuItemData = await File.ReadAllTextAsync("Data/SeedData/MenuItemSeedData.json");
        var MenuItems = JsonSerializer.Deserialize<List<CreateMenuItemDto>>(MenuItemData);

        foreach (var menuItem in MenuItems)
        {
            MenuItem item = new MenuItem
            {
                Description = menuItem.Description,
                Title = menuItem.Title,
                Price = menuItem.Price
            };
            menuItemRepository.Add(item, menuItem.CategoryId);
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