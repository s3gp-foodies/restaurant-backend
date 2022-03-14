using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories
{
    public class RepositoryOrderItem : IRepositoryMenuItems
    {
        private readonly DataContext _context;

        public RepositoryOrderItem(DataContext db)
        {
            _context = db;
        }
       

        public void Add(MenuItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(MenuItem item)
        {
            throw new NotImplementedException();
        }


        public void Edit(MenuItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<MenuItem> GetMenuItem(int id)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }
    }
}
