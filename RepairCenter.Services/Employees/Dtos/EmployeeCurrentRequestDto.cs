using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees.Dtos
{
    public class EmployeeCurrentRequestDto
    {
        public int RequestId { get; set; }

        public string RequestNumber { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string Device { get; set; } = null!;

        public RequestStatus Status { get; set; }
    }
}
