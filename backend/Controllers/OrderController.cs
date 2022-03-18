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
            foreach(orderItem in Order)
            {
                int id = orderItem.id;
                int productid = orderItem.productid;
                int quantity = orderItem.quantity;
                string image = orderItem.image;

            }

            return RedirectToAction("index");
        }

    }
}