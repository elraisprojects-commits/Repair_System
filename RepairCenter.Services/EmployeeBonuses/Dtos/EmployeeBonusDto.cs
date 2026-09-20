using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeBonuses.Dtos
{
    public class EmployeeBonusDto
    {
     
            public int Id { get; set; }

            public string EmployeeId { get; set; } = null!;

            public string EmployeeName { get; set; } = null!;

            public decimal BonusAmount { get; set; }

            public decimal DeductionAmount { get; set; }

            public decimal NetAmount { get; set; }

            public string? Reason { get; set; }

            public DateTime CreatedAt { get; set; }

            public string CreatedBy { get; set; } = null!;
        
    }
}