using foodies_app.Entities;
using foodies_app.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;

    public OrderRepository(DataContext context)
    {
        _context = context;
    }

    public Task<Order?> GetSessionOrder(int sessionId)
    {
        return _context.Orders.Where(order => order.SessionId == sessionId)
            .Include(o => o.Items)
            .FirstOrDefaultAsync();
    }
    
    public async Task<bool> ConfirmOrder(int orderId)
    {
        var order = await _context.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();
        if (order == null) throw new BadHttpRequestException("Order not found");
        order.Status = Status.inprogress;
        return (await _context.SaveChangesAsync()>0);

    }

    public Task<List<Order>> FetchOrders(int sessionId)
    {
        return _context.Orders.Where(order => order.SessionId == sessionId)
        .Include(o => o.Items)
        .ToListAsync();
    }

    public void Add(Order item, int Id)
    {
        _context.Orders.Add(item);
        _context.SaveChanges();
    }
}