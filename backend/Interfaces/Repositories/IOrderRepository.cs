using foodies_app.DTOs;
using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetSessionOrders(Session session);
        Task<Order?> GetOrderById(int id);
        Order CreateOrder(Session session, IEnumerable<OrderItem> newOrder);
        void UpdateOrder(Order order);

    }
}
