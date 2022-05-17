using AutoMapper;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Extensions;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace foodies_app.SignalR;

//[Authorize(Policy = "IsTable", Roles = "Admin")]
public class TableHub : Hub
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly List<Session> _sessions;

    public TableHub(UserManager<AppUser> userManager, IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _sessions = _unitOfWork.SessionRepository.GetAllSessionsNonAsync();
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.User == null) throw new HubException("No valid user found");
        var user = await _userManager.FindByNameAsync(Context.User.GetUsername());

        if (await _userManager.IsInRoleAsync(user, "Table"))
        {
            var session = await _unitOfWork.SessionRepository.GetSessionByUserId(user.Id) ??
                          await _unitOfWork.SessionRepository.StartSession(user);
            await _unitOfWork.Complete();
            _sessions.Add(session);
            var groupName = GetGroupName(user, session);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            if (session.Orders != null)
            {
                var currentOrders = session.Orders.ToList();

                //TODO: Send already submitted orders
            }
        }

        if (await _userManager.IsInRoleAsync(user, "Staff"))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "staff");
            //TODO: Send orders for status screen
        }
    }

    public async Task SubmitOrder(ICollection<SubmitProductDto> newOrder)
    {
        if (newOrder == null) throw new HubException("No order given");
        var session = GetUserSession();

        var order = _mapper.Map<Order>(newOrder);
        order.Completed = false;
        order.Status = Status.submitted;
        order.Session = session;
        order.OrderTime = DateTime.UtcNow;

        _unitOfWork.OrderRepository.CreateOrder(order, session);
        await _unitOfWork.Complete();
        await SendOrderToStaff(order);
    }

    private async Task SendOrderToStaff(Order order)
    {
        var submitOrder = _mapper.Map<SubmittedOrderDto>(order);
        await Clients.Group("staff").SendAsync("UpdateOrder", submitOrder);
    }

    public async Task<List<OrderDto>> GetOrders()
    {
        var session = GetUserSession();

        return await _unitOfWork.OrderRepository.GetSessionOrders(session);
    }

    public async Task RetrieveOrder()
    {
        var session = GetUserSession();

        var currentOrders = session.Orders.ToList();
        foreach (var order in currentOrders)
        {
            await Clients.Caller.SendAsync("Connected", order.Id, order.Status, order.Items, order.OrderTime);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    private string GetGroupName(AppUser user, Session session)
    {
        return user.UserName + "-" + session.Id;
    }

    private Session GetUserSession()
    {
        var session = _sessions.Find(x => x.UserId == Context.User.GetUserId());
        if (session == null) throw new HubException("No session found");
        return session;
    }
}