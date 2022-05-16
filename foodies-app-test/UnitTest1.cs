using foodies_app.Controllers;
using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace foodies_app_test;

public class MenuItemControllerTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var mock = new Mock<IUnitOfWork>();
        mock.Setup(work => work.MenuRepository.GetMenuItem(1).Result).Returns(new MenuItem()
        {
            Id = 1,
            Description = "salade",
            Price = 10,
            Title = "Ceasar salade",
            Category = null,
            Allergies = null,
        });

        var controller = new MenuController(mock.Object);
        var result = controller.GetItem(1).Result;
        Assert.That(result.Value.Title,Is.EqualTo("Ceasar salade"));
        Assert.That(result, Is.TypeOf(typeof(OkResult)));
    }
}