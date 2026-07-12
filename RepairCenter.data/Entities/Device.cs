using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class Device : BaseEntity
    {
        public string DeviceType { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string? SerialNumber { get; set; }



        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;


        public ICollection<ServiceRequest> ServiceRequests { get; set; }
        = new List<ServiceRequest>();
    }
}
