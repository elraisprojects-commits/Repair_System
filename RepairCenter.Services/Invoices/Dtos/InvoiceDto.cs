using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Invoices.Dtos
{
    public class InvoiceDto
    {
        public string RequestNumber { get; set; } = null!;

        public DateTime RequestDate { get; set; }

        // Customer
        public string CustomerName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Area { get; set; } = null!;

        // Device
        public string DeviceType { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string? SerialNumber { get; set; }

        // Request
        public string CustomerComplaint { get; set; } = null!;
        public string? ProblemCause { get; set; }

        public decimal? Cost { get; set; }

        public int? ExpectedDays { get; set; }

        public string BranchName { get; set; } = null!;

        public string ReceptionistName { get; set; } = null!;

        public string? SpecialistName { get; set; }

        public RequestStatus Status { get; set; }

        public string? DeliveredByName { get; set; }

        public DateTime? DeliveredAt { get; set; }
    }
}