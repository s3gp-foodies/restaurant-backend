using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories;


    public interface IMenuRepository
    {
        Task<List<MenuItem>> GetMenuItems();
        Task<MenuItem?> GetMenuItem(int id);
        void AddMenuItem(MenuItem item);
        void DeleteMenuItem(MenuItem item);
        void UpdateMenuItem(MenuItem item);
      
    }