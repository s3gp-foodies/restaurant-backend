using foodies_app.DTOs;

namespace foodies_app.Entities
{
    public class OrderItem
    {   
        public int Id { get; set; }
        public int Quantity { get; set; }

        public Status Status{ get; set; }
        
        public MenuItem MenuItem { get; set; }
        public int ItemId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }

        public OrderItem()
        {

        }

        public OrderItem(OrderItemDto OrderItemDto)
        {

            Id = OrderItemDto.Id;
            Quantity = OrderItemDto.Quantity;
            ItemId = OrderItemDto.ItemId;

        }
    }
    public enum Status
    {
        submitted,
        inprogress,
        complete
    }


}
