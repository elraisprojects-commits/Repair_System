using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
