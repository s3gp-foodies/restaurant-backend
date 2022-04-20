namespace foodies_app.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public DateTime OrderTime { get; set; }
    public List<OrderItemDto> Items { get; set; }
}