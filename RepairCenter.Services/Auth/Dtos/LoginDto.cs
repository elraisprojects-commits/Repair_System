using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Auth.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
