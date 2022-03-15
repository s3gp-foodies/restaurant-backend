using foodies_app.Entities;

namespace foodies_app.Interfaces
{
    public interface IAllergyRepository
    {
        Task<IEnumerable<Allergy>> GetAllergyCategories();
        Task<Allergy> GetAllergyCategory(Guid id);
        void Add(Allergy item);
        void Delete(Allergy item);
        void Edit(Allergy item);
    }
}
