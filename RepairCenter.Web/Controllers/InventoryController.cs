using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.Inventory;
using RepairCenter.Services.Inventory.Dtos;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

       
        // Add Item
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateInventoryItemDto dto)
        {
            await _inventoryService.CreateAsync(dto);

            return Ok(new
            {
                Message = "Item added successfully."
            });
        }

        
        // Update Item
       
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInventoryItemDto dto)
        {
            await _inventoryService.UpdateAsync(dto);

            return Ok(new
            {
                Message = "Item updated successfully."
            });
        }

       
        // Increase Quantity (+)
      
        [HttpPut("increase/{id}")]
        public async Task<IActionResult> IncreaseQuantity(int id)
        {
            await _inventoryService.IncreaseQuantityAsync(id);

            return Ok(new
            {
                Message = "Quantity increased successfully."
            });
        }

        
        // Decrease Quantity (-)
       
        [HttpPut("decrease/{id}")]
        public async Task<IActionResult> DecreaseQuantity(int id)
        {
            await _inventoryService.DecreaseQuantityAsync(id);

            return Ok(new
            {
                Message = "Quantity decreased successfully."
            });
        }

       
        // Soft Delete
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _inventoryService.DeleteAsync(id);

            return Ok(new
            {
                Message = "Item deleted successfully."
            });
        }

        
        // Get All Items
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _inventoryService.GetAllAsync();

            return Ok(items);
        }

        
        // Get Item By Id
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _inventoryService.GetByIdAsync(id);

            if (item == null)
                return NotFound(new
                {
                    Message = "Item not found."
                });

            return Ok(item);
        }

        
        // Warehouse Summary
        
        [HttpGet("summary")]
        public async Task<IActionResult> Summary()
        {
            var summary = await _inventoryService.GetSummaryAsync();

            return Ok(summary);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(

           InventoryFilterDto dto)
        {
            var result = await _inventoryService.FilterAsync(dto);

            return Ok(result);
        }
    }
}