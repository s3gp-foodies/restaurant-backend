using foodies_app.Entities;

namespace foodies_app.Interfaces;


    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem?>> GetOrderItems();
        Task<OrderItem?> GetOrderItem(Guid id);
        void Add(OrderItem item);
        void Delete(OrderItem item);
        void Edit(OrderItem item);
      

    }