using foodies_app.Entities;

namespace foodies_app.Interfaces
{
    public interface IRepositoryOrder
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        void Add(Order item);
        void Delete(Order item);
        void Edit(Order item);
    }
}
