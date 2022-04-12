using System.Collections.Generic;
using System.Threading.Tasks;
using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order item, int Id);
        Task<Order?> GetSessionOrder(int sessionId);
        Task<bool> ConfirmOrder(int orderId);
    }
}
