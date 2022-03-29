using NUnit.Framework;

namespace Back_End_Testing;

public class MenuItemUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var a = 10;
        Assert.AreEqual(a, 10);
    }
}