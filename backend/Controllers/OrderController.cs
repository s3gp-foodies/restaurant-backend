using Microsoft.AspNetCore.Mvc;
using foodies_app.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace foodies_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepositoryOrder _repositoryOrder;

        public OrderController(IRepositoryOrder repositoryOrder)
        {
            _repositoryOrder = repositoryOrder;
        }
                        
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> GetOrder(list Order)
        {
            Order = repositoryOrder.GetOrder;
            return View(Order);
        }

        // POST: api/<OrderController>
        public IEnumerable<string> ConfirmOrder(list Order)
        {
            Order = repositoryOrder.GetOrder;
            foreach(var Orderitem in Order)
            {
            // Int ID is voor de bestelling, niet het ID dat bij het orderitem hoort
            int id = 0;
            int itemid = Orderitem.id;
            int quantity = Orderitem.quantity;
            decimal total = Orderitem.total;
            repositoryOrder.AddOrder(id, itemid, quantity, total);
            }
            return RedirectToAction("index");
        }

    }
}