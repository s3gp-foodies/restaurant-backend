using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories;

public interface ISessionRepository
{
    Task<Session> StartSession(AppUser user);
    void EndSession(Session session);
    Task<Session?> GetSessionByUserId(int userId);
    Task<List<Session>> GetAllSessions();
    List<Session> GetAllSessionsNonAsync();
}