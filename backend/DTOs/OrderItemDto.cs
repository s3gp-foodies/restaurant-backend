namespace foodies_app.DTOs;

public class OrderItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int MenuItemId { get; set; }
}