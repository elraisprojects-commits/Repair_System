using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees.Dtos
{
    public class EmployeeProfileDto
    {
        // بيانات الموظف

        public string Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string NationalId { get; set; }

        public string Role { get; set; }

        public string? BranchName { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }


        // Bonus

        public decimal CurrentMonthBonus { get; set; }

        public decimal TotalBonus { get; set; }


        // Statistics

        public int CurrentRequests { get; set; }

        public int CompletedRequests { get; set; }

        public int DeliveredRequests { get; set; }

        public int CancelledRequests { get; set; }


        // آخر بونص

        public DateTime? LastBonusDate { get; set; }


      



        public List<EmployeeCurrentRequestDto> CurrentJobs
            = new();
    }
}