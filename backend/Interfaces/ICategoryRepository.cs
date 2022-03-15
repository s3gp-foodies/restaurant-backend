using foodies_app.Entities;

namespace foodies_app.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(Guid id);
        void Add(Category item);
        void Delete(Category item);
        void Edit(Category item);
    }
}
