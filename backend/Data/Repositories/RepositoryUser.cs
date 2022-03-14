using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories
{
    public class RepositoryUser
    {
        private readonly DataContext _context;

        public RepositoryUser(DataContext db)
        {
            _context = db;
        }
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Edit()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
