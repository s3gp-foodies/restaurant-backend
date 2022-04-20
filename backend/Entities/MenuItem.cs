using foodies_app.DTOs;

namespace foodies_app.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public ICollection<Allergy> Allergy { get; set; }
        public Category Category { get; set; }


        public MenuItem()
        {
            
        }

        public MenuItem(MenuItemDTO menuItemDto)
        {
            
            Title = menuItemDto.Title;
            Description = menuItemDto.Description;
            Price = menuItemDto.Price;
            
        }
    }
}