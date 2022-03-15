using Microsoft.AspNetCore.Identity;

namespace foodies_app.Entities;

public class AppUser : IdentityUser<Guid>
{
    //Add fields here if users need more info. Username, Id and Password are taken care of by Identity
    public ICollection<AppUserRole> UserRoles { get; set; }
}