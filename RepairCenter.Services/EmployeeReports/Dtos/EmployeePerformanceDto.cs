using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.EmployeeReports.Dtos
{
    public class EmployeePerformanceDto
    {
        public string EmployeeId { get; set; } = null!;

        public string EmployeeName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string? BranchName { get; set; }

        public int AssignedRequests { get; set; }

        public int CompletedRequests { get; set; }

        public int DeliveredRequests { get; set; }

        public int CancelledRequests { get; set; }

        public decimal TotalRevenue { get; set; }

       

        // Salary
        public decimal BasicSalary { get; set; }

        public decimal TotalBonus { get; set; }

        public decimal TotalDeduction { get; set; }

        public decimal NetSalary { get; set; }
    }
}