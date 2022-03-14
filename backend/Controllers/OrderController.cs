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
            foreach( in Order)
            {

            }
            int id = 0;
            string title = txtTitle;
            string artist = txtArtist;
            string link = txtLink;
            DateTime created = DateTime.Now;
            Albumlogic.AddAlbum(id, title, artist, link, created);
            return RedirectToAction("index");
        }

    }
}