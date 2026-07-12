using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Requests.Dtos
{
    public class RequestDetailsDto
    {
        public int RequestId { get; set; }

        public string RequestNumber { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string PhoneNumber1 { get; set; } = null!;

        public string? PhoneNumber2 { get; set; }

        public string Area { get; set; } = null!;

        public string DeviceType { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Model { get; set; } = null!;

        public string? SerialNumber { get; set; }

        public string CustomerComplaint { get; set; } = null!;

        public string? InspectionResult { get; set; }

        public string? ProblemCause { get; set; }

        public decimal? Cost { get; set; }

        public int? ExpectedDays { get; set; }

        public string ReceptionistName { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}

