using AutoMapper;
using AutoMapper.QueryableExtensions;
using foodies_app.DTOs;
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

    public async Task<List<OrderDto>> GetSessionOrders(Session session)
    {
        return await _context.Orders.Where(order => order.Session == session)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<Order?> GetOrderById(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public void CreateOrder(Order order, Session session)
    {
        _context.Orders.Add(order);
        session.Orders.Add(order);
    }

    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
    }
}