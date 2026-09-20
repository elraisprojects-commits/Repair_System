using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeBonuses.Dtos
{
    public class EmployeeBonusSummaryDto
    {
        public decimal TotalBonus { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetAmount { get; set; }

        public int TransactionsCount { get; set; }
    }
}
