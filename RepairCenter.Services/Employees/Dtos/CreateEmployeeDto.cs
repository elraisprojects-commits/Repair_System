using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees.Dtos
{
    public class CreateEmployeeDto
    {
        public string FullName { get; set; } = null!;

        public string NationalId { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public decimal Salary { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;

        public int? BranchId { get; set; }
    }
}
