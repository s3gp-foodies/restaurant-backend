﻿using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Interfaces;
using foodies_app.Interfaces.Repositories;
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
        public async Task<MenuItem> Get(int id)
        {
            return await _menuItemRepository.GetMenuItem(id);
        }

        // POST api/<MenuItemController>
        [HttpPost]
        public void Post([FromBody] MenuItemsDTO value)
        {
            // if (value.Description != null)
            // {
            MenuItem item = new MenuItem(value);
            
            _menuItemRepository.Add(item);

            //}
        }

        // PUT api/<MenuItemController>/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody] MenuItem menuitem)
        {
            var item = await _menuItemRepository.GetMenuItem(id);
            if (item.Description != null)
            {
                _menuItemRepository.Add(menuitem);
            }
        }

        // DELETE api/<MenuItemController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            var item = await _menuItemRepository.GetMenuItem(id);
            if (item != null)
            {
                _menuItemRepository.Delete(item);
            }
            //else send not succeeded or item not found respond
            
        }
    }
}