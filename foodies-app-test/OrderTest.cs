using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using foodies_app.Controllers;
using foodies_app.Data;
using foodies_app.Data.Repositories;
using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Interfaces;
using foodies_app.Interfaces.Repositories;
using foodies_app.SignalR;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;
using foodies_app.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace foodies_app_test;

public class OrderTest
{
    private readonly TableHub _hub;
    private readonly ISessionRepository _session;
    private readonly UserManager<AppUser> _userManager;
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test()
    {
       
        var mock = new Mock<UnitOfWork>();
        OrderDto neworder = new OrderDto()
        {
            OrderTime = DateTime.UtcNow,
            Items = new List<OrderItemDto>()
            {
                new OrderItemDto{
                    Id = 1,
                    Quantity = 3,
                    ItemId = 1
                }
            }
        };
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == "table1");
        await _session.StartSession(user);
        
        // _hub.SubmitOrder(neworder, );



        // mock.Setup(work => work.OrderRepository.CreateOrder());
    }
}