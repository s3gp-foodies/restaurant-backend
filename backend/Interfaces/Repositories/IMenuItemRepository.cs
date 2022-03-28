using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories;


    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem?>> GetMenuItems();
        Task<MenuItem?> GetMenuItem(int id);
        void Add(MenuItem item);
        void Delete(MenuItem item);
        void Edit(MenuItem item);
      
    }