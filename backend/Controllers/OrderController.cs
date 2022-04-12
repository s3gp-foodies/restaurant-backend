using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Interfaces.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace foodies_app.Controllers;

public class OrderController : BaseApiController
{
    private readonly Mapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public OrderController(Mapper mapper, IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }
                        
    [HttpGet("{sessionId}")]
    public async Task<ActionResult<List<OrderItemDto>>> GetSessionOrder(int sessionId)
    {
        var order = await _orderRepository.GetSessionOrder(sessionId);
        if (order == null) return BadRequest("No orders found for session");
        return _mapper.Map<List<OrderItem>, List<OrderItemDto>>(order.Items);
    }

    public async Task<ActionResult<List<OrderItemDto>>> FetchOrders(int sessionId)
    {
        var order = await _orderRepository.GetSessionOrder(sessionId);
        if (order == null) return BadRequest("No orders found for session");
        return _mapper.Map<List<OrderItem>, List<OrderItemDto>>(order.Items);
    }

    [HttpPut("confirm/{orderId}")]
    public async Task<ActionResult> ConfirmOrder(int orderId)
    {
        if (await _orderRepository.ConfirmOrder(orderId))
        {
            return Ok();
        }

        return BadRequest("Something went wrong");
        
    }

}