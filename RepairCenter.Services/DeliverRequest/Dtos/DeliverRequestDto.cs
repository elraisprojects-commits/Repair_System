using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.DeliverRequest.Dtos
{
    public class DeliverRequestDto
    {
        public int RequestId { get; set; }

        public string DeliveredByName { get; set; } = null!;
    }

}
