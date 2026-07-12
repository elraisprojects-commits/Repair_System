using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class InventoryItem : BaseEntity
    {
        public string ItemCode { get; set; } = null!;      // ITM-000001

        public int ItemSequence { get; set; }

        public string ItemName { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
