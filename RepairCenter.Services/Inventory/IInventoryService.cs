using RepairCenter.Services.Inventory.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inventory
{
    public interface IInventoryService
    {
        Task CreateAsync(CreateInventoryItemDto dto);

        Task UpdateAsync(UpdateInventoryItemDto dto);

        Task IncreaseQuantityAsync(int itemId);

        Task DecreaseQuantityAsync(int itemId);

        Task DeleteAsync(int itemId);

        Task<List<InventoryListDto>> GetAllAsync();

        Task<InventoryItemDto?> GetByIdAsync(int itemId);

        Task<InventorySummaryDto> GetSummaryAsync();

        Task<List<InventoryListDto>> FilterAsync(InventoryFilterDto dto);
    }
}