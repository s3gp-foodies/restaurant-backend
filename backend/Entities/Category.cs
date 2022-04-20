using foodies_app.DTOs;

namespace foodies_app.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(CategoryDto dto)
        {
            Name = dto.Name;
        }

        public Category()
        {
            
        }
    }
}
