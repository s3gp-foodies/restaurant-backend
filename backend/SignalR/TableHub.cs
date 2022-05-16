using AutoMapper;
using foodies_app.Data;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Extensions;
using foodies_app.Interfaces;
using foodies_app.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace foodies_app.SignalR;

//[Authorize(Policy = "IsTable", Roles = "Admin")]
public class TableHub : Hub
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ISessionRepository _sessionRepository;
    public readonly IUnitOfWork _unitofwork;
    private readonly IMapper _mapper;
    public readonly List<Session> _sessions;

    public TableHub(ISessionRepository sessionRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _userManager = userManager;
        _unitofwork = unitOfWork;
        _mapper = mapper;
        _sessions = _sessionRepository.GetAllSessionsNonAsync();
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.User == null) throw new HubException("No valid user found");
        var user = await _userManager.FindByNameAsync(Context.User.GetUsername());

        var session = await _sessionRepository.GetSessionByUserId(user.Id) ??
                      await _sessionRepository.StartSession(user);
        var groupName = GetGroupName(user, session);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);


        var CurrentOrders = session.Orders.ToList();
        foreach (var order in CurrentOrders)
        {
            var message = order;
            await Clients.Caller.SendAsync("Connected", message.Id, message.Status, message.Items, message.OrderTime);
        }
    }

    public async Task SubmitOrder(OrderNewDto orderNewDto)
    {
        if (orderNewDto == null) throw new HubException("No order given");
        var session = _sessions.Find(x => x.UserId == Context.User.GetUserId());
        if (session == null) throw new HubException("no session found");

        var order = _mapper.Map<Order>(orderNewDto);
        order.Completed = false;
        order.Status = Status.submitted;
        order.Session = session;
        order.OrderTime = DateTime.UtcNow;
        
        _unitofwork.OrderRepository.CreateOrder(order, session);
        _unitofwork.Complete();  
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    private string GetGroupName(AppUser user, Session session)
    {
        return user.UserName + "-" + session.Id;
    }
}