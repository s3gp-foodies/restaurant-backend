using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories
{
    public interface IAllergyRepository
    {
        Task<IEnumerable<Allergy>> GetAllergyCategories();
        Task<Allergy> GetAllergyCategory(int id);
        void Add(Allergy item);
        void Delete(Allergy item);
        void Edit(Allergy item);
    }
}
