using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inventory.Dtos
{
    public class InventoryListDto
    {
        public int Id { get; set; }

        public string ItemCode { get; set; } = null!;

        public string ItemName { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
