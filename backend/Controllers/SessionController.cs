using AutoMapper;
using foodies_app.Entities;
using foodies_app.Extensions;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace foodies_app.Controllers;

[Authorize]
public class SessionController :BaseApiController
{
    private readonly Mapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public SessionController(Mapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    [HttpPost("start")]
    public async Task<ActionResult<Session>> StartSession()
    {
        var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());
        return await _unitOfWork.SessionRepository.StartSession(user);
    }
    
    [HttpPost("end")]
    public void EndSession(Session session)
    {
        _unitOfWork.SessionRepository.EndSession(session);
    }
}