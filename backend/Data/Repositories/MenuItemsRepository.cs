using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories
{
    public class MenuItemRepository : IMenuItemRepository

    {
        private readonly DataContext _context;
        private readonly ICategoryRepository _categoryRepository;

        public MenuItemRepository(DataContext db , ICategoryRepository categoryRepository)
        {
            _context= db;
            _categoryRepository = categoryRepository;
        }

        public void Add(MenuItem item, int menuItemCategoryId)
        {
            // fix needed how to fill in category
            item.Category = _categoryRepository.GetCategory(menuItemCategoryId);
            _context.MenuItems.Add(item);
            _context.SaveChanges();
        }

        public void Delete(MenuItem item)
        { 
            // MenuItem menuItem =_context.MenuItems.Find(item);
            // _context.MenuItems.Remove(menuItem);
            // _context.SaveChanges();
        }

        public void Edit(MenuItem item)
        {
            _context.MenuItems.Update(item);
            _context.SaveChanges();
        }

        public async Task<MenuItem> GetMenuItem(int id)
        {
            return await _context.MenuItems.Include("Category").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<MenuItem>> GetMenuItems()
        {
            return await _context.MenuItems.Include("Category").ToListAsync();
        }
    }
}
