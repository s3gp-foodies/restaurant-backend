using foodies_app.Entities;

namespace foodies_app.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}