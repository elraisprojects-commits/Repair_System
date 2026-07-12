using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? NationalId { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; } = true;

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

    }
}
