using System.Text;
using foodies_app.Data;
using foodies_app.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace foodies_app.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<AppUser>(opt =>
            {
                //This is default but as an example of how to configure
                opt.Password.RequireNonAlphanumeric = true;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddRoleValidator<RoleValidator<AppRole>>()
            .AddEntityFrameworkStores<DataContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                //For normal validation
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false, //Would validate the server 
                    ValidateAudience = false //Would validate the Angular app
                };
        
                //For SignalR validation
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
        
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
        
                        return Task.CompletedTask;
                    }
                };
            });

        //Use this to configure policies that can be used in API endpoints
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("IsAdmin", policy => policy.RequireRole("Admin"));
            opt.AddPolicy("IsTable", policy => policy.RequireRole("Table"));
            opt.AddPolicy("IsStaff", policy => policy.RequireRole("Staff"));
        });

        return services;
    }
}