using foodies_app.DTOs;

namespace foodies_app.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Allergy> Allergies { get; set; }
        public Category Category { get; set; }

    }
}