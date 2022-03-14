using foodies_app.Entities;

namespace foodies_app.Interfaces
{
    public interface IRepositoryAllergyCategory
    {
        Task<IEnumerable<AllergyCategory>> GetAllergyCategories();
        Task<AllergyCategory> GetAllergyCategory(int id);
        void Add(AllergyCategory item);
        void Delete(AllergyCategory item);
        void Edit(AllergyCategory item);
    }
}
