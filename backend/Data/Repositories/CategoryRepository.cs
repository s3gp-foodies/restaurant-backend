using foodies_app.Entities;
using foodies_app.Interfaces.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext db, IMapper mapper)
        {
            _context= db;
        }
        public async Task<List<Category>> GetCategories()
        {
           return await _context.Categories.OrderBy(cat => cat.Id).ToListAsync();
        }

        public async Task<Category?> GetCategory(int id)
        {
           return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(Category item)
        {
            _context.Categories.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Category item)
        {
            _context.Categories.Remove(item);
            _context.SaveChanges();
        }

        public void Edit(Category item)
        {
            _context.Categories.Update(item);
            _context.SaveChanges();
        }
    }
}
