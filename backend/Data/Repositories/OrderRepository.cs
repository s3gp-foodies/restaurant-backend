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

    public List<OrderItem> GetAllOrdersById(int id)
    {
        return _context.OrderItems.Where(x => x.OrderId == id).ToList();
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

    public async Task UpdateOrderStatus(int id, Status status)
    {
        var order = await _context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
        if (order == null) throw new HubException("Order doesn't exist");
        order.Status = status;
        if (status == Status.complete) order.Completed = true;
    }


    public async Task UpdateOrderItemStatus(int id, Status status)
    {
        var orderItem = await _context.OrderItems.Where(o => o.Id == id).FirstOrDefaultAsync();
        if (orderItem == null) throw new HubException("OrderItem doesn't exist");
        orderItem.Status = status;

        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderItem.OrderId);
        if (order != null && order.Items.TrueForAll(i => i.Status == Status.complete))
        {
            order.Status = Status.complete;
            order.Completed = true;
        }
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

    public async Task<List<Order>> GetAllOrders()
    {
        // List<OrderItem> orderitems = _context.OrderItems.ToList();

        List<Order> orders = _context.Orders.ToList();
        // foreach (var order in orders)
        // {
        //  order.Items.Add(orderitems.Find(x => x.OrderId == order.Id));
        // }

        return orders;
    }

    public async Task<List<SubmittedOrderDto>> GetStaffOrders()
    {
        List<Order> orders = _context.Orders.Include(o => o.Session).Include(s => s.Session.User).ToList();
        List<SubmittedOrderDto> staffOrders = new List<SubmittedOrderDto>();

        foreach (var order in orders)
        {
            List<SubmittedProductDto> submittedProducts = new List<SubmittedProductDto>();
            List<OrderItem> orderItems = GetAllOrdersById(order.Id);

            orderItems.ForEach(orderItem =>
            {
                _context.MenuItems.Include("Category")
                    .FirstOrDefaultAsync(menuItem => menuItem.Id == orderItem.MenuItemId);
            });

            orderItems.ForEach(x => submittedProducts.Add(new SubmittedProductDto
            {
                Id = x.Id,
                Name = x.MenuItem.Title,
                Category = x.MenuItem.Category.Name,
                Amount = x.Quantity
            }));

            string userName = order.Session.User.UserName;
            staffOrders.Add(new SubmittedOrderDto()
            {
                tableId = int.Parse(userName.Remove(0, 5)),
                time = order.OrderTime,
                products = submittedProducts
            });
        }

        return staffOrders;
    }
    
    public void ClearAllOrders()
    {
        System.Diagnostics.Debug.WriteLine("Orders:");
        System.Diagnostics.Debug.WriteLine(_context.Orders);
    }
}