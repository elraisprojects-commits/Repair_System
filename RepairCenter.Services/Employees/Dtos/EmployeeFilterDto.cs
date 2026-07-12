using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees.Dtos
{
    public class EmployeeFilterDto
    {
        public string? FullName { get; set; }

        public string? Role { get; set; }
    }
}