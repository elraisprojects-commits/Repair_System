using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.Inventory.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InventoryService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Add Item
        public async Task CreateAsync(CreateInventoryItemDto dto)
        {
            var lastSequence = await _context.InventoryItem
                .MaxAsync(x => (int?)x.ItemSequence);

            var nextSequence = (lastSequence ?? 0) + 1;

            var item = _mapper.Map<InventoryItem>(dto);

            item.ItemSequence = nextSequence;

            item.ItemCode = $"ITM-{nextSequence:D6}";

            item.IsActive = true;

            item.TotalPrice = item.Quantity * item.UnitPrice;

            _context.InventoryItem.Add(item);

            await _context.SaveChangesAsync();
        }

        // Update Item
        public async Task UpdateAsync(UpdateInventoryItemDto dto)
        {
            var item = await _context.InventoryItem
                .FirstOrDefaultAsync(x =>
                    x.Id == dto.Id &&
                    x.IsActive);

            if (item == null)
                throw new Exception("Item not found.");

            item.ItemName = dto.ItemName;

            item.UnitPrice = dto.UnitPrice;

            item.TotalPrice = item.Quantity * item.UnitPrice;

            await _context.SaveChangesAsync();
        }

        // Increase Quantity
        public async Task IncreaseQuantityAsync(int itemId)
        {
            var item = await _context.InventoryItem
                .FirstOrDefaultAsync(x =>
                    x.Id == itemId &&
                    x.IsActive);

            if (item == null)
                throw new Exception("Item not found.");

            item.Quantity++;

            item.TotalPrice = item.Quantity * item.UnitPrice;

            await _context.SaveChangesAsync();
        }

        // Decrease Quantity
        public async Task DecreaseQuantityAsync(int itemId)
        {
            var item = await _context.InventoryItem
                .FirstOrDefaultAsync(x =>
                    x.Id == itemId &&
                    x.IsActive);

            if (item == null)
                throw new Exception("Item not found.");

            if (item.Quantity == 0)
                throw new Exception("Quantity is already zero.");

            item.Quantity--;

            item.TotalPrice = item.Quantity * item.UnitPrice;

            await _context.SaveChangesAsync();
        }

        // Soft Delete
        public async Task DeleteAsync(int itemId)
        {
            var item = await _context.InventoryItem
                .FirstOrDefaultAsync(x =>
                    x.Id == itemId &&
                    x.IsActive);

            if (item == null)
                throw new Exception("Item not found.");

            item.IsActive = false;

            await _context.SaveChangesAsync();
        }

        // Get All
        public async Task<List<InventoryListDto>> GetAllAsync()
        {
            var items = await _context.InventoryItem
                .Where(x => x.IsActive)
                .OrderBy(x => x.ItemName)
                .ToListAsync();

            return _mapper.Map<List<InventoryListDto>>(items);
        }

        // Get By Id
        public async Task<InventoryItemDto?> GetByIdAsync(int itemId)
        {
            var item = await _context.InventoryItem
                .FirstOrDefaultAsync(x =>
                    x.Id == itemId &&
                    x.IsActive);

            if (item == null)
                return null;

            return _mapper.Map<InventoryItemDto>(item);
        }

        // Warehouse Summary
        public async Task<InventorySummaryDto> GetSummaryAsync()
        {
            return new InventorySummaryDto
            {
                TotalItemsCount = await _context.InventoryItem
                    .CountAsync(x => x.IsActive),

                TotalQuantity = await _context.InventoryItem
                    .Where(x => x.IsActive)
                    .SumAsync(x => x.Quantity),

                TotalWarehousePrice = await _context.InventoryItem
                    .Where(x => x.IsActive)
                    .SumAsync(x => x.TotalPrice)
            };
        }
        public async Task<List<InventoryListDto>> FilterAsync(
    InventoryFilterDto dto)
        {
            var query = _context.InventoryItem
                .Where(x => x.IsActive)
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(dto.Search))
            {
                query = query.Where(x =>

                    x.ItemName.Contains(dto.Search)

                    || x.ItemCode.Contains(dto.Search)

                    || x.UnitPrice.ToString().Contains(dto.Search)

                    || x.Quantity.ToString().Contains(dto.Search)

                    || x.TotalPrice.ToString().Contains(dto.Search));
            }
            switch (dto.Filter)
            {
                case InventoryFilter.InStock:
                    query = query.Where(x => x.Quantity > 0);
                    break;

                case InventoryFilter.OutOfStock:
                    query = query.Where(x => x.Quantity == 0);
                    break;
            }

            // Date Filter
            if (dto.FromDate.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt.Date >= dto.FromDate.Value.Date);
            }

            if (dto.ToDate.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt.Date <= dto.ToDate.Value.Date);
            }

            // Execute Query
            var items = await query
                .OrderBy(x => x.ItemName)
                .ToListAsync();

            return _mapper.Map<List<InventoryListDto>>(items);
        }
    }
}
