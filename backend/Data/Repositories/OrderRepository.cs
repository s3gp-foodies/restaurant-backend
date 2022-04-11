using AutoMapper;
using foodies_app.Entities;
using foodies_app.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace foodies_app.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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

}