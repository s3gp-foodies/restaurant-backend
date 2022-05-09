namespace foodies_app.DTOs;

public class MenuItemUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public int Id { get; set; }
}