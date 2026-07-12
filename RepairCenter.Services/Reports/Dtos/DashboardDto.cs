using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Reports.Dtos
{
    public class DashboardDto
    {
        // Requests
        public int TotalRequests { get; set; }

        public int Received { get; set; }

        public int UnderReview { get; set; }

        public int WaitingCustomerApproval { get; set; }

        public int InProgress { get; set; }

        public int Completed { get; set; }

        public int Delivered { get; set; }

        public int CompanyRejected { get; set; }

        public int CancelledByCustomer { get; set; }

        public int TotalCancelled { get; set; }

        // Customers
        public int TotalCustomers { get; set; }

        // Devices
        public int TotalDevices { get; set; }

        // Employees
        public int TotalEmployees { get; set; }

        public int TotalAdmins { get; set; }

        public int TotalReceptionists { get; set; }

        public int TotalSpecialists { get; set; }

        // Inventory
        public int TotalInventoryItems { get; set; }

        public int OutOfStockItems { get; set; }

        public decimal TotalInventoryValue { get; set; }

        // Revenue
        public decimal TotalRevenue { get; set; }

        public decimal AverageRepairCost { get; set; }
    }
}