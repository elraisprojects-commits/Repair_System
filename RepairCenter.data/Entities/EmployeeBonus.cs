using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class EmployeeBonus : BaseEntity
    {
        public string EmployeeId { get; set; } = null!;
        public ApplicationUser Employee { get; set; } = null!;

        public decimal Amount { get; set; }

        public string? Reason { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string CreatedById { get; set; } = null!;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}