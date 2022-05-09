using foodies_app.DTOs;
using foodies_app.Entities;
using foodies_app.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace foodies_app.Controllers
{
    [Authorize]
    public class MenuController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuItemDto>>> GetMenu()
        {
            
            return await _unitOfWork.MenuRepository.GetMenuItems();
        }
        
        [HttpGet("categories")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await _unitOfWork.CategoryRepository.GetCategories();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MenuItem>> GetItem(int id)
        {
            var item = await _unitOfWork.MenuRepository.GetMenuItem(id);
            if (item == null) return NotFound("Item not found");
            return item;
        }

        [HttpPost("new")]
        public async Task<ActionResult> NewItem([FromBody] MenuItemNewDto dto)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategory(dto.CategoryId);
            if (category == null) return NotFound("The category does not exist");
            var item = new MenuItem
            {
                Description = dto.Description,
                Title = dto.Title,
                Price = dto.Price,
                Category = category
            };

            _unitOfWork.MenuRepository.AddMenuItem(item);
            return await _unitOfWork.Complete() ? Ok() : BadRequest("Something went wrong when saving");
        }

        [HttpPut("update/")]
        public async Task<ActionResult> UpdateItem([FromBody] MenuItemUpdateDto updatedItem)
        {
            var menuItem = await _unitOfWork.MenuRepository.GetMenuItem(updatedItem.Id);
            if (menuItem == null) return NotFound("Cannot update an item that doesn't exist.");

            menuItem.Title = updatedItem.Title;
            menuItem.Price = updatedItem.Price;
            menuItem.Description = updatedItem.Description;

            if (menuItem.Category.Id != updatedItem.CategoryId)
            {
                var category = await _unitOfWork.CategoryRepository.GetCategory(updatedItem.CategoryId);
                if (category == null) return NotFound("The category does not exist");
                menuItem.Category = category;
            }

            _unitOfWork.MenuRepository.UpdateMenuItem(menuItem);
            return await _unitOfWork.Complete() ? Ok() : BadRequest("Something went wrong when saving");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _unitOfWork.MenuRepository.GetMenuItem(id);
            if (item == null) return NotFound("Item not found");

            _unitOfWork.MenuRepository.DeleteMenuItem(item);
            return await _unitOfWork.Complete() ? Ok() : BadRequest("Something went wrong when saving");

        }
    }
}