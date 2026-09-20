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

        public decimal BonusAmount { get; set; }

        public decimal DeductionAmount { get; set; }

        public string? Reason { get; set; }

        

        public string CreatedById { get; set; } = null!;
        public ApplicationUser CreatedBy { get; set; } = null!;
    }
}