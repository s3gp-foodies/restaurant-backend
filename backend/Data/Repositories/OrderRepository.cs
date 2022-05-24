using AutoMapper;
using AutoMapper.QueryableExtensions;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Interfaces.Repositories;
using Microsoft.AspNetCore.SignalR;
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

    public Order CreateOrder(Session session, IEnumerable<OrderItem> orderItems)
    {
        var order = _context.Orders.Add(new Order
        {
            OrderTime = DateTime.UtcNow,
            Completed = false,
            Session = session,
            Status = Status.submitted,
            Items = new List<OrderItem>()
        });
        order.Entity.Items.AddRange(orderItems);
        session.Orders.Add(order.Entity);
        
        return order.Entity;
    }

    public void UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
    }

    // private void CreateOrderItems(Order order, ICollection<SubmitProductDto> newOrder)
    // {
    //     foreach (var newOrderItem in newOrder)
    //     {
    //         var menuItem = _context.MenuItems.Find(newOrderItem.ProductId);
    //         if (menuItem == null) throw new HubException("Invalid product");
    //         var orderitem = new OrderItem()
    //         {
    //             Quantity = newOrderItem.Count,
    //             Status = Status.submitted,
    //             MenuItem = menuItem,
    //             MenuItemId = menuItem.Id
    //         };
    //         order.Items.Add(orderitem);
    //     }
    // }
}