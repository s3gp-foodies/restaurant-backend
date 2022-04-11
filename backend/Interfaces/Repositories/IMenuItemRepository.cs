using System.Collections.Generic;
using System.Threading.Tasks;
using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories;


    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetMenuItems();
        Task<MenuItem?> GetMenuItem(int id);
        void Add(MenuItem item, int menuItemCategoryId);
        void Delete(MenuItem item);
        void Edit(MenuItem item);
      
    }