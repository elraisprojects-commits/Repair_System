using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Requests.Dtos
{
    public class CreateRequestDto
    {
        // Customer

        public string CustomerName { get; set; } = null!;

        public string PhoneNumber1 { get; set; } = null!;

        public string? PhoneNumber2 { get; set; }

        public string Area { get; set; } = null!;


        // Device

        public string DeviceType { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string? SerialNumber { get; set; }


        // Request

        public int BranchId { get; set; }

        public string CustomerComplaint { get; set; } = null!;
        public string ReceptionistName { get; set; } = null!;

       // public string RequestNumber { get; set; } = null!;
    }
}
