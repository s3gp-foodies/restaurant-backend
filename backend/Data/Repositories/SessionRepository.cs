using AutoMapper;
using foodies_app.Entities;
using foodies_app.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public SessionRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Session> StartSession(AppUser user)
    {
        var session = new Session
        {
            User = user,
            Start = DateTime.UtcNow
        };
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }

    public async void EndSession(Session session)
    {
        session.End = DateTime.UtcNow;
        _context.Sessions.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task<Session?> GetSessionByUserId(int userId)
    {
        return await _context.Sessions.Where(s => s.User.Id == userId).FirstOrDefaultAsync();
    }

    public async Task<List<Session>> GetAllSessions()
    {
        return await _context.Sessions.ToListAsync();
    }
    
    public List<Session> GetAllSessionsNonAsync()
    {
        return _context.Sessions.ToList();
    }
}