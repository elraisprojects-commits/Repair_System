using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees.Dtos
{
    public class UpdateEmployeeDto
    {
        public string Id { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string NationalId { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public decimal Salary { get; set; }

        public int? BranchId { get; set; }
    }
}