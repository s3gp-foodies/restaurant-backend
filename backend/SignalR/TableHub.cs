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

        var session = await _unitOfWork.SessionRepository.GetSessionByUserId(user.Id) ??
                      await _unitOfWork.SessionRepository.StartSession(user);
        var groupName = GetGroupName(user, session);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);


        var currentOrders = session.Orders.ToList();
        foreach (var order in currentOrders)
        {
            await Clients.Caller.SendAsync("Connected", order.Id, order.Status, order.Items, order.OrderTime);
        }
    }

    public async Task SubmitOrder(OrderNewDto orderNewDto)
    {
        if (orderNewDto == null) throw new HubException("No order given");
        var session = GetUserSession();

        var order = _mapper.Map<Order>(orderNewDto);
        order.Completed = false;
        order.Status = Status.submitted;
        order.Session = session;
        order.OrderTime = DateTime.UtcNow;

        _unitOfWork.OrderRepository.CreateOrder(order, session);
        await _unitOfWork.Complete();
    }

    public async Task<List<OrderDto>> GetOrders()
    {
        var session = GetUserSession();

        return await _unitOfWork.OrderRepository.GetSessionOrders(session);
    }


    public override async Task OnDisconnectedAsync(Exception exception)
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