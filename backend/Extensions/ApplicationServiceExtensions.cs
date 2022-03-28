using foodies_app.Data;
using foodies_app.Data.Repositories;
using foodies_app.Helpers;
using foodies_app.Interfaces;
using foodies_app.Interfaces.Repositories;
using foodies_app.Services;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // services.AddSingleton<PresenceTracker>(); - this is for websocket, currently disables cause not implemented

        services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);

        //AddScoped means that the service exists for the duration of the HTTP request
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        
        //Add the database context
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(config.GetConnectionString("DatabaseConnection"));
        });
        
        return services;
    }
}