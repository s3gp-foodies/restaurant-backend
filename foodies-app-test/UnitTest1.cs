using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using foodies_app.Controllers;
using foodies_app.Data;
using foodies_app.Entities;
using foodies_app.Interfaces;
using NUnit.Framework;
using Moq;
using foodies_app.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace foodies_app_test;

public class MenuItemControllerTests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void GetMenuItem_From_Id()
    {
        var menuRepoMock = new Mock<IUnitOfWork>();
        menuRepoMock.Setup(unitOfWork => unitOfWork.MenuRepository.GetMenuItem(1).Result).Returns(new MenuItem()
        {
            Id = 1,
            Description = "salade",
            Price = 10,
            Title =" ceaser salade",
            Category = null,
            Allergy = null,
        });
        ;
        var controller  = new MenuItemController(menuRepoMock.Object);
        var result = controller.GetItem(1);
        Assert.That(result.Result.Value.Title, Is.EqualTo("ceaser salade"));
        
    }

    [Test]
    public void GetMenuITem_From_id_failure()
    {
        var menuRepoMock = new Mock<IUnitOfWork>();
        menuRepoMock.Setup(unitOfWork => unitOfWork.MenuRepository.GetMenuItem(1).Result).Returns(new MenuItem()
        {
            Id = 1,
            Description = "salade",
            Price = 10,
            Title =" ceaser salade",
            Category = null,
            Allergy = null,
        });
        ;
        var controller  = new MenuItemController(menuRepoMock.Object);
        var result = controller.GetItem(2);
        Assert.That(result.Result.Result, Is.InstanceOf(typeof(NotFoundObjectResult)));
    }
    [Test]
    public void GetMenuItems_Equall_to_Three()
    {
        var menuRepoMock = new Mock<IUnitOfWork>();
        menuRepoMock.Setup(unitOfWork => unitOfWork.MenuRepository.GetMenuItems().Result).Returns(new List<MenuItem>()
        {
            new MenuItem()
            {
                Id = 1,
                Description = "salade",
                Price = 10,
                Title = " ceaser salade",
                Category = null,
                Allergy = null,
            },
            new MenuItem()
            {
                Id = 2,
                Description = "broodje kip rijk gevuld aan salade en malse kip",
                Price = 5,
                Title = "broodje kip",
                Category = null,
                Allergy = null,
            },
            new MenuItem()
            {
            Id = 3,
            Description = "lekkere gekoelde frits kola minder zoet maar even zo lekker",
            Price = 3,
            Title = " frits kola",
            Category = null,
            Allergy = null,
        }
        });
        var controller  = new MenuItemController(menuRepoMock.Object);
        var result = controller.GetMenu();
       Assert.AreEqual(3,result.Result.Value.Count);
    }

    [Test]
    public void GetMenuItems_Equall_to_Three_Failure()
    {
        var menuRepoMock = new Mock<IUnitOfWork>();
        menuRepoMock.Setup(unitOfWork => unitOfWork.MenuRepository.GetMenuItems().Result).Returns(new List<MenuItem>()
        {
            
        });
        var controller  = new MenuItemController(menuRepoMock.Object);
        var result = controller.GetMenu();
        Assert.That(result.Result.Result, Is.Null);
    }
   
}