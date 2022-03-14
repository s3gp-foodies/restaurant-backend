using foodies_app.Entities;

namespace foodies_app.Interfaces;


    public interface IRepositoryOrderItem
    {
        Task<IEnumerable<OrderItem>> GetOrderItems();
        Task<OrderItem> GetOrderItem(int id);
        void Add(OrderItem item);
        void Delete(OrderItem item);
        void Edit(OrderItem item);
      

    }