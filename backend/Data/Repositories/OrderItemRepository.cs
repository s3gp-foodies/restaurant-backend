using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DataContext _context;

        public OrderItemRepository(DataContext db)
        {
            _context = db;
        }
       

        public void Add(OrderItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderItem item)
        {
            throw new NotImplementedException();
        }


        public void Edit(OrderItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderItem?> GetOrderItem(Guid id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<OrderItem?>> GetOrderItems()
        {
            return await _context.OrderItems.ToListAsync();
        }
    }
}
