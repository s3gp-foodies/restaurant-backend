namespace foodies_app.DTOs
{
    public class CreateMenuItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
