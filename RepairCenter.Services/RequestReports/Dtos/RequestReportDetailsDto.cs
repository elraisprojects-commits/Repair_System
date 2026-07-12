using RepairCenter.data.Enums;
using RepairCenter.Services.Invoices.Dtos;
using RepairCenter.Services.RequestNotes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestReports.Dtos
{
    public class RequestReportDetailsDto
    {
        public int RequestId { get; set; }

        public string RequestNumber { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Device { get; set; } = null!;

        public string Branch { get; set; } = null!;

        public string Receptionist { get; set; } = null!;

        public string? Specialist { get; set; }

        public RequestStatus Status { get; set; }

        public string? InspectionResult { get; set; }

        public decimal? Cost { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public List<AddRequestNoteDto> Notes { get; set; } = new();

        public List<InvoiceDto> InvoiceItems { get; set; } = new();
    }
}