using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeReports.Dtos
{
    public class EmployeeReportResponseDto
    {
        public List<EmployeePerformanceDto> Employees { get; set; }
            = new();

        public int TotalEmployees { get; set; }

        public int TotalAssignedRequests { get; set; }

        public int TotalCompletedRequests { get; set; }

        public int TotalDeliveredRequests { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}