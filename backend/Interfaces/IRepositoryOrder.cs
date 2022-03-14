using foodies_app.Entities;

namespace foodies_app.Interfaces
{
    public interface IRepositoryOrder
    {
        Task<IEnumerable<Order>> GetOrder();
        Task<IEnumerable<Order>> ConfirmOrder();
    }
}
