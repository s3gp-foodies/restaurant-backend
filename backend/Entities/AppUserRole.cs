using Microsoft.AspNetCore.Identity;

namespace foodies_app.Entities;

public class AppUserRole : IdentityUserRole<Guid>
{
    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}