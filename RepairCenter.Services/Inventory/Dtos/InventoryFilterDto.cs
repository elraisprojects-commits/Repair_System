using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inventory.Dtos
{
    public class InventoryFilterDto
    {
        public string? Search { get; set; }

        public InventoryFilter Filter { get; set; } = InventoryFilter.All;

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
