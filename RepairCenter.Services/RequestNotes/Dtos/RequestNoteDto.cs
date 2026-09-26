using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestNotes.Dtos
{
    public class RequestNoteDto
    {
        public int Id { get; set; }

        public int RequestId { get; set; }

        public string? Note { get; set; }

        public RequestStatus Status { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public DateTime? CancelledAt { get; set; }
    }
}

