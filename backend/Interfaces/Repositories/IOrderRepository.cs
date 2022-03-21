using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        void Add(Order item);
        void Delete(Order item);
        void Edit(Order item);
    }
}
