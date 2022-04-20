using foodies_app.Entities;
using foodies_app.Interfaces.Repositories;

namespace foodies_app.Data.Repositories
{
    public class AllergyRepository : IAllergyRepository
    {
        public Task<IEnumerable<Allergy>> GetAllergyCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Allergy> GetAllergyCategory(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Allergy item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Allergy item)
        {
            throw new NotImplementedException();
        }

        public void Edit(Allergy item)
        {
            throw new NotImplementedException();
        }
    }
}
