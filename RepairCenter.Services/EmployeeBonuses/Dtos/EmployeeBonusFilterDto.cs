using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeBonuses.Dtos
{
    public class EmployeeBonusFilterDto
    {
        public string? EmployeeId { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }
    }
}