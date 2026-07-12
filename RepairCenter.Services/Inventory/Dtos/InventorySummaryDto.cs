using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inventory.Dtos
{
    public class InventorySummaryDto
    {
        public int TotalItemsCount { get; set; }

        public int TotalQuantity { get; set; }

        public decimal TotalWarehousePrice { get; set; }
    }
}
