using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories;


    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem?>> GetOrderItems();
        Task<OrderItem?> GetOrderItem(int id);
        void Add(OrderItem item, int itemId);
        void Delete(OrderItem item);
        void Edit(OrderItem item);
        
    }