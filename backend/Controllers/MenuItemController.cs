using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace foodies_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IRepositoryMenuItems _repositoryMenuItems;

        public MenuItemController(IRepositoryMenuItems repositoryMenuItems)
        {
            _repositoryMenuItems = repositoryMenuItems;
        }
        // GET: api/<MenuItemController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAll()
        {
            return NotImplementedException(); //(IEnumerable<MenuItem>)_repositoryMenuItems.GetMenuItems();
        }

        private ActionResult<IEnumerable<MenuItem>> NotImplementedException()
        {
            throw new NotImplementedException();
        }

        // GET api/<MenuItemController>/5
        [HttpGet("{id}")]
       
        public async Task<MenuItem> Get(int id)
        {
            if (id != 0)
            {
                return await _repositoryMenuItems.GetMenuItem(id);
            }
           else
            {
                //make badrequest
                MenuItem item = new MenuItem();
                return item;
            }
        }

        // POST api/<MenuItemController>
        [HttpPost]
        public void Post([FromBody] MenuItem value)
        {
            if (value.Category != null && value.Description != null && value.Title != null)
            {
                _repositoryMenuItems.Add(value);
            }
            else BadRequest();
        }

        // PUT api/<MenuItemController>/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] MenuItem menuitem)
        {
            MenuItem item = await _repositoryMenuItems.GetMenuItem(id);
            if(item.Description != null && item.Title !=null)
                {
                _repositoryMenuItems.Add(menuitem);
                }
        }

        // DELETE api/<MenuItemController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            MenuItem item = await _repositoryMenuItems.GetMenuItem(id);
            if(item.Id != 0 && item.Title != null)
            {
                _repositoryMenuItems.Delete(item);
            }

        }
    }
}
