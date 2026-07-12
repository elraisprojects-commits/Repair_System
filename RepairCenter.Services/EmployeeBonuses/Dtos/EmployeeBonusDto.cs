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

        public decimal Amount { get; set; }

        public string? Reason { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}