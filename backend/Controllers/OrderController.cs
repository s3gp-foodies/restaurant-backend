using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Extensions;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace foodies_app.Controllers;

[Authorize]
public class OrderController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderController(IMapper mapper, IUnitOfWork unitOfWork)
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
        
        return  await _unitOfWork.OrderRepository.GetSessionOrders(session);
    }

    //TODO: Fix this
    [HttpPost("submit")]
    public async Task<ActionResult> SubmitOrder(OrderSubmissionDto orderSubmission)
    {  
        var userId = User.GetUserId();
        if (userId == null) return BadRequest();
        
        var session = await _unitOfWork.SessionRepository.GetSessionByUserId((int) userId);
        if (session == null) return BadRequest("No session for this table");
        
        var items = (List<OrderItem>) orderSubmission.Items.Select(os => new OrderItem
        {
            MenuItemId = os.ItemId,
            Quantity = os.Quantity
        });
        
        var order = new Order
        {
            Items = items
            };
        
      //  _unitOfWork.OrderRepository.CreateOrder(order, session);
        return await _unitOfWork.Complete() ? Ok() : BadRequest("Something went wrong when saving");
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
    [HttpGet("getallorders")]
    public async Task<List<Order>> GetAllOrders()
    {
        return await _unitOfWork.OrderRepository.GetAllOrders();
    }
    
    [HttpPost("clearAllOrders")]
    public void ClearAllOrders()
    {
        _unitOfWork.OrderRepository.ClearAllOrders();
    }

    [HttpGet("getAllStaffOrders")]
    public async Task<List<SubmittedOrderDto>> GetAllStaffOrders()
    {
        return await _unitOfWork.OrderRepository.GetStaffOrders();
    }
}