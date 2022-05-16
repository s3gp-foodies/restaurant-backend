using foodies_app.Entities;

namespace foodies_app.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<AllergyDto> Allergies { get; set; }
        public CategoryDto Category { get; set; }

    }
}