namespace foodies_app.DTOs;

public class GetOrderItemDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ItemId { get; set; }
}