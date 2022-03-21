using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories
{
    public class MenuItemRepository : IMenuItemRepository

    {
        private readonly DataContext _context;

        public MenuItemRepository(DataContext db)
        {
            _context= db;
        }

        public void Add(MenuItem item)
        {
            _context.MenuItems.Add(item);
        }

        public void Delete(MenuItem item)
        {
            _context.MenuItems.Remove(item);
        }

        public void Edit(MenuItem item)
        {
            _context.MenuItems.Update(item);
            _context.SaveChanges();
        }

        public async Task<MenuItem> GetMenuItem(int id)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MenuItem?>> GetMenuItems()
        {
            return await _context.MenuItems.ToListAsync();
        }
    }
}
