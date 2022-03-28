﻿using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Category GetCategory(int id);
        void Add(Category item);
        void Delete(Category item);
        void Edit(Category item);
    }
}