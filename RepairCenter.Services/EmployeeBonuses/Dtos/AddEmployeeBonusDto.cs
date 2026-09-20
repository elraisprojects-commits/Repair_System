using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeBonuses.Dtos
{
    public class AddEmployeeBonusDto
    {
        public string EmployeeId { get; set; } = null!;

        public decimal BonusAmount { get; set; }

        public decimal DeductionAmount { get; set; }

        public string? Reason { get; set; }
    }
}