using System.Collections.Generic;
using System.Threading.Tasks;
using foodies_app.Entities;

namespace foodies_app.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetSessionOrder(int sessionId);
        Task<bool> ConfirmOrder(int orderId);
    }
}
