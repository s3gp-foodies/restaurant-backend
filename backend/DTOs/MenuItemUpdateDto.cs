namespace foodies_app.DTOs;

public class MenuItemUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int categoryId { get; set; }
    public int Id { get; set; }
}