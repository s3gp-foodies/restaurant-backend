using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Extensions;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace foodies_app.Controllers;

[Authorize]
public class OrderController : BaseApiController
{
    private readonly Mapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(Mapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
                        
    //TODO: Check if this works. For now we need to be able to create one
    [HttpGet("orders")]
    public async Task<ActionResult<List<OrderDto>>> GetSessionOrders()
    {
        var userId = User.GetUserId();
        if (userId == null) return BadRequest();
        
        var session = await _unitOfWork.SessionRepository.GetSessionByUserId((int) userId);
        if (session == null) return BadRequest("No session for this table");
        
        var orders = await _unitOfWork.OrderRepository.GetSessionOrders(session);
        
        return _mapper.Map<List<Order>, List<OrderDto>>(orders);
    }
    
    // [HttpPut("update/")]
    // public async Task<ActionResult> ConfirmOrder(int orderId)
    // {
    //     if (await _unitOfWork.OrderRepository.UpdateOrder(orderId))
    //     {
    //         return Ok();
    //     }
    //
    //     return BadRequest("Something went wrong");
    //     
    // }

}