using foodies_app.Entities;

namespace foodies_app.Interfaces
{
    public interface IRepositoryOrder
    {
        List<OrderItem> GetOrder();
        Task<IEnumerable<Order>> AddOrder(int id, int itemid, int quantity, decimal total);
    }
}
