using System.Text.Json;
using AutoMapper;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data;

public static class Seed
{
    private static IUnitOfWork _unitOfWork;
    private static UserManager<AppUser> _userManager;
    private static RoleManager<AppRole> _roleManager;
    private static Mapper _mapper;

    //Add new seeds by creating a seed data file Data/SeedData/*.json
    //and creating a Seed* method that takes the relevant services as parameters.
    public static async Task Run(IServiceScope scope)
    {
        _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        _mapper = scope.ServiceProvider.GetRequiredService<Mapper>();
        await SeedUsers();
        await SeedCategories();
        await SeedMenuItems();
        await _unitOfWork.Complete();
    }

    private static async Task SeedCategories()
    {
        var categoryData = await File.ReadAllTextAsync("Data/SeedData/CategorySeedData.json");
        var categories = JsonSerializer.Deserialize<List<CategoryDto>>(categoryData);

        if (categories != null)
            foreach (var category in categories)
            {
                _unitOfWork.CategoryRepository.Add(_mapper.Map<Category>(category));
            }
    }

    private static async Task SeedMenuItems()
    {
        var menuItemData = await File.ReadAllTextAsync("Data/SeedData/MenuItemSeedData.json");
        if (menuItemData == null) throw new ArgumentNullException(nameof(menuItemData));
        var menuItems = JsonSerializer.Deserialize<List<MenuItemNewDto>>(menuItemData);

        if (menuItems != null)
            foreach (var menuItem in menuItems)
            {
                var category = await _unitOfWork.CategoryRepository.GetCategory(menuItem.CategoryId);
                var item = new MenuItem
                {
                    Description = menuItem.Description,
                    Title = menuItem.Title,
                    Price = menuItem.Price,
                    Category = category
                };

                _unitOfWork.MenuRepository.AddMenuItem(item);
            }
    }

    private static async Task SeedUsers()
    {
        if (await _userManager.Users.AnyAsync()) return;

        var roles = new List<AppRole>
        {
            new() {Name = "Staff"},
            new() {Name = "Admin"},
            new() {Name = "Table"},
        };

        foreach (var role in roles)
        {
            await _roleManager.CreateAsync(role);
        }

        var userData = await File.ReadAllTextAsync("Data/SeedData/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<RegisterDto>>(userData);

        if (users != null)
            foreach (var user in users)
            {
                var appUser = new AppUser
                {
                    UserName = user.UserName.ToLower()
                };
                await _userManager.CreateAsync(appUser, "Passw0rd!");
                await _userManager.AddToRoleAsync(appUser, user.Role);
            }
    }
}