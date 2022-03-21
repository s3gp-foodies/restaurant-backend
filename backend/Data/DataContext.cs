using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace foodies_app.Data;

using Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole,
    IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Allergy> Allergies { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Table> Tables { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasMany(u => u.UserRoles)
            .WithOne(r => r.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
        builder.Entity<AppRole>()
            .HasMany(r => r.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(r => r.RoleId)
            .IsRequired();

        builder.Entity<Session>()
            .HasOne(s => s.User)
            .WithOne(u => u.Session);
        builder.Entity<Session>()
            .HasMany(s => s.Orders)
            .WithOne(o => o.Session);
    }
}