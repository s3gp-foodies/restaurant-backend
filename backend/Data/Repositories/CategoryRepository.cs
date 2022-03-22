using foodies_app.Entities;
using foodies_app.Interfaces;

namespace foodies_app.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext db)
        {
            _context= db;
        }
        public Task<IEnumerable<Category>> GetCategories()
        {
            _context.Categories
            throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
           return _context.Categories.FirstOrDefault(x => x.Id == id);
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
