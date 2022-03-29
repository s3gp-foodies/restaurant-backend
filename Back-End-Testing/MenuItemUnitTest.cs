using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using foodies_app.Controllers;
using foodies_app.Data;
using foodies_app.Data.Repositories;
using foodies_app.Entities;
using foodies_app.Interfaces;
using NUnit.Framework;

namespace Back_End_Testing
{
    public class MenuItemUnitTest
    {
        private readonly MenuItemRepository _menItemRepo;
        private readonly IMenuItemRepository _iMenuItemRepo;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var controller = new MenuItemController(_iMenuItemRepo);
            List<MenuItem>  items = controller.GetAll();
            Assert.Equals(items.Capacity, 0);
        }
    }
}
