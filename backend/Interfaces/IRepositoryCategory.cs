using foodies_app.Entities;

namespace foodies_app.Interfaces
{
    public interface IRepositoryCategory
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        void Add(Category item);
        void Delete(Category item);
        void Edit(Category item);
    }
}
