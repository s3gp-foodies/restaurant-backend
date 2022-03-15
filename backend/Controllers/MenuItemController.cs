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
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemController(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
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
        public async Task<MenuItem> Get(Guid id)
        {
            return await _menuItemRepository.GetMenuItem(id);
        }

        // POST api/<MenuItemController>
        [HttpPost]
        public void Post([FromBody] MenuItem value)
        {
            if (value.Description != null)
            {
                _menuItemRepository.Add(value);
            }
            else BadRequest("Description of item is empty");
        }

        // PUT api/<MenuItemController>/5
        [HttpPut("{id}")]
        public async void Put(Guid id, [FromBody] MenuItem menuitem)
        {
            var item = await _menuItemRepository.GetMenuItem(id);
            if (item.Description != null)
            {
                _menuItemRepository.Add(menuitem);
            }
        }

        // DELETE api/<MenuItemController>/5
        [HttpDelete("{id}")]
        public async void Delete(Guid id)
        {
            var item = await _menuItemRepository.GetMenuItem(id);
            _menuItemRepository.Delete(item);
        }
    }
}