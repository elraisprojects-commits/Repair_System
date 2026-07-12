using RepairCenter.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Auth
{

    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user);

    }
}
