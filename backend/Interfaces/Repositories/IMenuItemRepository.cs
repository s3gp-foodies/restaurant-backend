using System.Collections.Generic;
using System.Threading.Tasks;
using foodies_app.Entities;


namespace foodies_app.Interfaces;


    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetMenuItems();
        Task<MenuItem?> GetMenuItem(int id);
        void Add(MenuItem item);
        void Delete(MenuItem item);
        void Edit(MenuItem item);
      
    }