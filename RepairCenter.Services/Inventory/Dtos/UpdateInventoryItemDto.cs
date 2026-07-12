using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inventory.Dtos
{
    public class UpdateInventoryItemDto
    {
        public int Id { get; set; }

        public string ItemName { get; set; } = null!;

        public decimal UnitPrice { get; set; }
    }
}
