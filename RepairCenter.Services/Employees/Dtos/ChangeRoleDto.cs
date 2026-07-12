using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Employees.Dtos
{
    public class ChangeRoleDto
    {
        public string UserId { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}

