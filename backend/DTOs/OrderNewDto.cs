namespace foodies_app.DTOs;

public class OrderNewDto
{
   public ICollection<OrderItemDto[]> Items { get; set; }
}