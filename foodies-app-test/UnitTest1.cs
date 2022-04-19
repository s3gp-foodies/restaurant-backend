using NUnit.Framework;
using Moq;
using foodies_app.Interfaces.Repositories;

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
        var menuRepoMock = new Mock<IMenuRepository>();
        
    }
}