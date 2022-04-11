using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using foodies_app.Entities;
using foodies_app.Interfaces;
using foodies_app.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories
{
    public class MenuRepository : IMenuRepository

    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MenuRepository(DataContext db, IMapper mapper)
        {
            _context= db;
            _mapper = mapper;
        }

        public void AddMenuItem(MenuItem item)
        {
            _context.MenuItems.Add(item);
        }

        public void DeleteMenuItem(MenuItem item)
        { 
            _context.MenuItems.Remove(item);
        }

        public void UpdateMenuItem(MenuItem item)
        {
            _context.MenuItems.Update(item);
        }

        public async Task<MenuItem> GetMenuItem(int id)
        {
            return await _context.MenuItems.Include("Category").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<MenuItem>> GetMenuItems()
        {
            return await _context.MenuItems.Include("Category").ToListAsync();
        }
    }
}
