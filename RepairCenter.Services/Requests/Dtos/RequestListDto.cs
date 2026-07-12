using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Requests.Dtos
{
    public class RequestListDto
    {
        public int RequestId { get; set; }

        public string RequestNumber { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string DeviceType { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string ReceptionistName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}

