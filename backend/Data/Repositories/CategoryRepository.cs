using foodies_app.Entities;
using foodies_app.Interfaces;

namespace foodies_app.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategory(Guid id)
        {
            throw new NotImplementedException();
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
