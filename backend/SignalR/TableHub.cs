using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Extensions;
using foodies_app.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace foodies_app.SignalR;

//[Authorize(Policy = "IsTable", Roles = "Admin")]
public class TableHub : Hub
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ISessionRepository _sessionRepository;

    public TableHub(ISessionRepository sessionRepository, UserManager<AppUser> userManager)
    {
        _sessionRepository = sessionRepository;
        _userManager = userManager;
    }

    public override async Task OnConnectedAsync()
    {
        if (Context.User == null) throw new HubException("No valid user found");
        var user = await _userManager.FindByNameAsync(Context.User.GetUsername());

        var session = await _sessionRepository.GetSessionByUserId(user.Id) ?? await _sessionRepository.StartSession(user);
        var groupName = GetGroupName(user, session);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        //TODO: Replace with function to send current orders
        var message = "Connected with session " + groupName;
        await Clients.Caller.SendAsync("Connected", message);
    }

    public async Task SubmitOrder(OrderNewDto orderNewDto)
    {
        
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