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
            throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
           return _context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Category item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category item)
        {
            throw new NotImplementedException();
        }

        public void Edit(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
