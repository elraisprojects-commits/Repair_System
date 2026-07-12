using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeReports.Dtos
{
    public class EmployeeReportFilterDto
    {
        public string? EmployeeName { get; set; }

        public string? Role { get; set; }

        public int? BranchId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
